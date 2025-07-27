# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all .csproj files
COPY ["AuthService.Api/AuthService.Api.csproj", "AuthService.Api/"]
COPY ["AuthService.Application/AuthService.Application.csproj", "AuthService.Application/"]
COPY ["AuthService.Domain/AuthService.Domain.csproj", "AuthService.Domain/"]
COPY ["AuthService.Infrastructure/AuthService.Infrastructure.csproj", "AuthService.Infrastructure/"]
COPY ["AuthService.sln", "./"]

# Restore dependencies
RUN dotnet restore "AuthService.Api/AuthService.Api.csproj"

# Copy the rest of the source code
COPY . .

# Publish the app
WORKDIR "/src/AuthService.Api"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y netcat-traditional

COPY --from=build /app/publish .

COPY AuthService.Api/wait-for-sql.sh /wait-for-sql.sh
RUN chmod +x /wait-for-sql.sh

ENTRYPOINT ["/wait-for-sql.sh", "sqlserver_db", "1433", "dotnet", "AuthService.Api.dll"]
