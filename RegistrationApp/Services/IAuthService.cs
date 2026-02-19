using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}