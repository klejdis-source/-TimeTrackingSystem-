using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Services;

public class OrarService : IOrarService
{
    private readonly AppDbContext _db;

    public OrarService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<WorkSchedule>> GetAllAsync() =>
        await _db.Orars
            .Where(o => o.IsActive)
            .ToListAsync();

    public async Task<WorkSchedule?> GetByIdAsync(int id) =>
        await _db.Orars.FirstOrDefaultAsync(o => o.Id == id);

    public async Task<WorkSchedule> CreateAsync(WorkSchedule orar)
    {
        orar.CreatedAt = DateTime.UtcNow;
        orar.ExpectedHours = (orar.EndTime - orar.StartTime).TotalHours;
        _db.Orars.Add(orar);
        await _db.SaveChangesAsync();
        return orar;
    }

    public async Task<WorkSchedule?> UpdateAsync(int id, WorkSchedule orar)
    {
        var existing = await _db.Orars.FindAsync(id);
        if (existing is null) return null;

        existing.Name = orar.Name;
        existing.Description = orar.Description;
        existing.StartTime = orar.StartTime;
        existing.EndTime = orar.EndTime;
        existing.ExpectedHours = (orar.EndTime - orar.StartTime).TotalHours;
        existing.WorkDays = orar.WorkDays;
        existing.IsActive = orar.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Orars.FindAsync(id);
        if (existing is null) return false;

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }
}