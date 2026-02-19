using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.Services;

public interface IAttendanceService
{
    Task<AttendanceResponse> ClockInAsync(int userId);
    Task<AttendanceResponse> ClockOutAsync(int userId);
    Task<IEnumerable<AttendanceResponse>> GetByUserAsync(int userId);
    Task<IEnumerable<AttendanceResponse>> GetAllAsync();
}