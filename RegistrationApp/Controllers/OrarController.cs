using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Services;

namespace TimeTrackingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")] 
public class OrarController : ControllerBase
{
    private readonly IOrarService _orar;
    public OrarController(IOrarService orar) => _orar = orar;

    [HttpGet]
    [Authorize(Roles = "Schedule_access")] 
    public async Task<IActionResult> GetAll() => Ok(await _orar.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Schedule_access")]
    public async Task<IActionResult> GetById(int id)
    {
        var orar = await _orar.GetByIdAsync(id);
        return orar is null ? NotFound(new { message = "Orari nuk u gjet." }) : Ok(orar);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkSchedule orar)
    {
        var created = await _orar.CreateAsync(orar);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] WorkSchedule orar)
    {
        var updated = await _orar.UpdateAsync(id, orar);
        return updated is null ? NotFound(new { message = "Orari nuk u gjet." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _orar.DeleteAsync(id);
        return ok ? NoContent() : NotFound(new { message = "Orari nuk u gjet." });
    }
}