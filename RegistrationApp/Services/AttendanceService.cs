using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendance;
    private readonly IEmployeeRepository _users;

    public AttendanceService(IAttendanceRepository attendance, IEmployeeRepository users)
    {
        _attendance = attendance;
        _users = users;
    }

    public async Task<AttendanceResponse> ClockInAsync(int userId)
    {
        var user = await _users.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("Punonjësi nuk u gjet.");

        if (!user.IsActive)
            throw new InvalidOperationException("Punonjësi është joaktiv.");

        var active = await _attendance.GetActiveByUserAsync(userId);
        if (active is not null)
            throw new InvalidOperationException("Punonjësi ka tashmë clock-in aktiv.");

        var now = DateTime.UtcNow;

        bool isLate = false;
        int lateMinutes = 0;
        if (user.Orari?.StartTime != null)
        {
            var expected = now.Date + user.Orari.StartTime;
            if (now > expected)
            {
                isLate = true;
                lateMinutes = (int)(now - expected).TotalMinutes;
            }
        }

        var record = new Attendance
        {
            EmployeeId = userId,
            Date = now.Date,
            CheckIn = now,
            WorkedHours = 0,
            Status = "Present",
            IsLate = isLate,
            LateMinutes = lateMinutes,
            CreatedAt = now
        };

        var created = await _attendance.CreateAsync(record);
        created.Employee = user;
        return ToResponse(created);
    }

    public async Task<AttendanceResponse> ClockOutAsync(int userId)
    {
        var user = await _users.GetByIdAsync(userId)
            ?? throw new KeyNotFoundException("Punonjësi nuk u gjet.");

        var active = await _attendance.GetActiveByUserAsync(userId);
        if (active is null)
            throw new InvalidOperationException("Punonjësi nuk ka clock-in aktiv.");

        var now = DateTime.UtcNow;

        bool isEarlyLeave = false;
        int earlyLeaveMinutes = 0;
        if (user.Orari?.EndTime != null)
        {
            var expected = now.Date + user.Orari.EndTime;
            if (now < expected)
            {
                isEarlyLeave = true;
                earlyLeaveMinutes = (int)(expected - now).TotalMinutes;
            }
        }

        active.CheckOut = now;
        active.WorkedHours = (now - active.CheckIn).TotalHours;
        active.IsEarlyLeave = isEarlyLeave;
        active.EarlyLeaveMinutes = earlyLeaveMinutes;
        active.UpdatedAt = now;

        await _attendance.UpdateAsync(active);
        active.Employee = user;
        return ToResponse(active);
    }

    public async Task<IEnumerable<AttendanceResponse>> GetByUserAsync(int userId)
    {
        var records = await _attendance.GetByUserAsync(userId);
        return records.Select(ToResponse);
    }

    public async Task<IEnumerable<AttendanceResponse>> GetAllAsync()
    {
        var records = await _attendance.GetAllAsync();
        return records.Select(ToResponse);
    }

    private static AttendanceResponse ToResponse(Attendance a) => new()
    {
        Id = a.Id,
        EmployeeId = a.EmployeeId,
        FullName = $"{a.Employee.FirstName} {a.Employee.LastName}",
        Date = a.Date,
        CheckIn = a.CheckIn,
        CheckOut = a.CheckOut,
        WorkedHours = a.WorkedHours,
        Status = a.Status,
        IsLate = a.IsLate,
        LateMinutes = a.LateMinutes
    };
}