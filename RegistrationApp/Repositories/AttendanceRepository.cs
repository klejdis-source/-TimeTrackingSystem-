using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _db;
    public AttendanceRepository(AppDbContext db) => _db = db;

    public async Task<Attendance?> GetActiveByUserAsync(int userId) =>
        await _db.Attendances.FirstOrDefaultAsync(a => a.EmployeeId == userId && a.CheckOut == null);

    public async Task<IEnumerable<Attendance>> GetByUserAsync(int userId) =>
        await _db.Attendances
            .Include(a => a.Employee)
            .Where(a => a.EmployeeId == userId)
            .OrderByDescending(a => a.CheckIn)
            .ToListAsync();

    public async Task<IEnumerable<Attendance>> GetByUserAndPeriodAsync(int userId, DateTime from, DateTime to) =>
        await _db.Attendances
            .Include(a => a.Employee)
            .Where(a => a.EmployeeId == userId && a.Date >= from && a.Date <= to)
            .OrderBy(a => a.Date)
            .ToListAsync();

    public async Task<IEnumerable<Attendance>> GetAllAsync() =>
        await _db.Attendances
            .Include(a => a.Employee)
            .OrderByDescending(a => a.CheckIn)
            .ToListAsync();

    public async Task<Attendance> CreateAsync(Attendance attendance)
    {
        _db.Attendances.Add(attendance);
        await _db.SaveChangesAsync();
        return attendance;
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        _db.Attendances.Update(attendance);
        await _db.SaveChangesAsync();
    }
}