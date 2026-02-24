using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BCrypt.Net;
using System.Text;
using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Services;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _users;
    private readonly IConfiguration _config;

    public AuthService(IEmployeeRepository users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    public async Task<LoginResponse?> LoginAsync(LoginDto request)
    {
        var user = await _users.GetByEmailAsync(request.Email);
        if (user is null || !user.IsActive) return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Roles.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        //kalosh dhe rolin
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            user.Roles.ToString(),
            $"{user.FirstName} {user.LastName}"
        );
    }
    public async Task<bool> RegisterAsync(SignUpRequest request)
    {
        var existing = await _users.GetByEmailAsync(request.Email);
        if (existing is not null) return false;

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Pozition = request.Pozition,
       
            Roles=request.Roles,
            IsActive = true,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            DepartmentId = request.DepartmentId,
            OrariId = request.OrariId
        };

        await _users.CreateAsync(employee);
        return true;
    }
}