using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
///[Authorize(Roles = "Admin")]
public class ReportController : ControllerBase
{
    private readonly IReportService _report;

    public ReportController(IReportService report) => _report = report;

    /// Merr orët e punës për të gjithë punonjësit
    [HttpGet("hours")]
    public async Task<IActionResult> GetAllHours()
    {
        var result = await _report.GetAllUsersHoursAsync();
        return Ok(result);
    }

    /// Merr raportin e orëve për një punonjës në një periudhë
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

    /// Merr të gjitha overtimes
    [HttpGet("overtime")]
    public async Task<IActionResult> GetAllOvertime()
    {
        var result = await _report.GetAllOvertimeAsync();
        return Ok(result);
    }

    /// Shton overtime për një punonjës
    [HttpPost("overtime")]
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
