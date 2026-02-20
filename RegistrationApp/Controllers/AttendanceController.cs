using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendance;

    public AttendanceController(IAttendanceService attendance) => _attendance = attendance;

    /// Clock-in - punonjësi regjistron hyrjen
    [HttpPost("clockin")]
    public async Task<IActionResult> ClockIn()
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _attendance.ClockInAsync(userId);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// Clock-out - punonjësi regjistron daljen
    [HttpPost("clockout")]
    public async Task<IActionResult> ClockOut()
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _attendance.ClockOutAsync(userId);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// Merr rekordet e attendance për punonjësin e kyçur
    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _attendance.GetByUserAsync(userId);
        return Ok(result);
    }

    /// Merr rekordet e attendance për një punonjës specifik (Admin only)
    [HttpGet("user/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByUser(int id)
    {
        try
        {
            var result = await _attendance.GetByUserAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// Merr të gjitha rekordet e attendance (Admin only)
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _attendance.GetAllAsync();
        return Ok(result);
    }
}