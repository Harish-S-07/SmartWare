using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWare.AuthService.Application.DTO;
using SmartWare.AuthService.Domain.Entities;

namespace SmartWare.AuthService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task LogoutAsync(string token);
        Task<List<User>> GetAllUsersAsync();
    }
}
