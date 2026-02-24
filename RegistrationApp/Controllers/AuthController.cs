using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result = await _auth.LoginAsync(request);
        if (result is null)
            return Unauthorized(new { message = "Email ose fjalëkalimi është i gabuar." });
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var result = await _auth.RegisterAsync(request);
        if (!result)
            return BadRequest(new { message = "Email ekziston tashmë." });
        return Ok(new { message = "Përdoruesi u regjistrua me sukses." });
    }

    [HttpGet("hash-test")]
    public IActionResult GetHash()
    {
        return Ok(BCrypt.Net.BCrypt.HashPassword("test"));
    }
}