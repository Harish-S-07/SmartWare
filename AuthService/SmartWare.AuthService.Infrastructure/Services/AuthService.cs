using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartWare.AuthService.Application.DTO;
using SmartWare.AuthService.Application.Interfaces;
using SmartWare.AuthService.Domain.Entities;
using SmartWare.AuthService.Persistence.Data;
using StackExchange.Redis;

namespace SmartWare.AuthService.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _db;
        private readonly IConfiguration _config;
        private readonly IConnectionMultiplexer _redis;

        public AuthService(AuthDbContext db, IConfiguration config, IConnectionMultiplexer redis)
        {
            _db = db;
            _config = config;
            _redis = redis;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _db.Users
                        .Select(u => new User
                        {
                            Id = u.Id,
                            Email = u.Email
                        })
                        .ToListAsync();

            return users;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateToken(user);
        }

        public async Task LogoutAsync(string token)
        {
            var redis = _redis.GetDatabase();
            var expiry = DateTimeOffset.UtcNow.AddHours(1);
            await redis.StringSetAsync($"blacklist_{token}", "true", expiry - DateTimeOffset.UtcNow);
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == request.Email);
            if (exists) throw new Exception("User already exists");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return GenerateToken(user);
        }

        private AuthResponse GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
