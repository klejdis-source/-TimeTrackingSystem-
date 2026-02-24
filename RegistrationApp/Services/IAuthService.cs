using Microsoft.AspNetCore.Identity.Data;
using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginDto request);
    Task<bool> RegisterAsync(SignUpRequest request);
}