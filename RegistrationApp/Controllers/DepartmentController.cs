using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;


[ApiController]
[Route("api/[controller]")]
///[Authorize(Roles = "Admin")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departments;

    public DepartmentController(IDepartmentService departments) => _departments = departments;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departments.GetAllAsync();
        return Ok(departments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dept = await _departments.GetByIdAsync(id);
        return dept is null ? NotFound(new { message = "Departamenti nuk u gjet." }) : Ok(dept);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Department department)
    {
        var created = await _departments.CreateAsync(department);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Department department)
    {
        var updated = await _departments.UpdateAsync(id, department);
        return updated is null ? NotFound(new { message = "Departamenti nuk u gjet." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _departments.DeleteAsync(id);
        return ok ? NoContent() : NotFound(new { message = "Departamenti nuk u gjet." });
    }
}

