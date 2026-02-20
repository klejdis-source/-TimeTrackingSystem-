using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _auth.LoginAsync(request);
        if (result is null)
            return Unauthorized(new { message = "Email ose fjalëkalimi është i gabuar." });

        return Ok(result);
    }
}
