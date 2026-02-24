using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Report_access")] // Admin + Employee me Report_access
public class ReportController : ControllerBase
{
    private readonly IReportService _report;
    public ReportController(IReportService report) => _report = report;

    [HttpGet("hours")]
    public async Task<IActionResult> GetAllHours() => Ok(await _report.GetAllUsersHoursAsync());

    [HttpPost("period")]
    public async Task<IActionResult> GetPeriodReport([FromBody] PeriodReportRequest request)
    {
        try
        {
            var result = await _report.GetUserPeriodReportAsync(request);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("overtime")]
    public async Task<IActionResult> GetAllOvertime() => Ok(await _report.GetAllOvertimeAsync());

    [HttpPost("overtime")]
    [Authorize(Roles = "Admin")] // vetem Admin shton overtime
    public async Task<IActionResult> AddOvertime([FromBody] CreateOvertimeRequest request)
    {
        try
        {
            var result = await _report.AddOvertimeAsync(request);
            return CreatedAtAction(nameof(GetAllOvertime), result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}