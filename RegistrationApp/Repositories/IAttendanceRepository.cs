using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Repositories.Interfaces;

public interface IAttendanceRepository
{
    Task<Attendance?> GetActiveByUserAsync(int userId);
    Task<IEnumerable<Attendance>> GetByUserAsync(int userId);
    Task<IEnumerable<Attendance>> GetByUserAndPeriodAsync(int userId, DateTime from, DateTime to);
    Task<IEnumerable<Attendance>> GetAllAsync();
    Task<Attendance> CreateAsync(Attendance attendance);
    Task UpdateAsync(Attendance attendance);
}