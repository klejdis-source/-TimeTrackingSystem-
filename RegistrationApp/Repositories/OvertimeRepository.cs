using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Repositories;

public class OvertimeRepository : IOvertimeRepository
{
    private readonly AppDbContext _db;
    public OvertimeRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Overtime>> GetAllAsync() =>
        await _db.Overtimes
            .Include(o => o.Employee)
            .OrderByDescending(o => o.Date)
            .ToListAsync();

    public async Task<IEnumerable<Overtime>> GetByUserAsync(int userId) =>
        await _db.Overtimes
            .Include(o => o.Employee)
            .Where(o => o.EmployeeId == userId)
            .OrderByDescending(o => o.Date)
            .ToListAsync();

    public async Task<Overtime> CreateAsync(Overtime overtime)
    {
        _db.Overtimes.Add(overtime);
        await _db.SaveChangesAsync();
        return overtime;
    }
}