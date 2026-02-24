using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // vetem Admin menaxhon punonjësit
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employees;
    public EmployeeController(IEmployeeService employees) => _employees = employees;

    [HttpGet]
    public async Task<IEnumerable<EmployeeResponse>> GetAllEmployee()
    {
        return await _employees.GetAllAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _employees.GetByIdAsync(id);
        return employee is null ? NotFound(new { message = "Punonjësi nuk u gjet." }) : Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        try
        {
            var created = await _employees.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeReqUest request)
    {
        var updated = await _employees.UpdateAsync(id, request);
        return updated is null ? NotFound(new { message = "Punonjësi nuk u gjet." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var ok = await _employees.DeactivateAsync(id);
        return ok ? NoContent() : NotFound(new { message = "Punonjësi nuk u gjet." });
    }
}