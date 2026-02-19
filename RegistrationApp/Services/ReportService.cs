using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Services;

public class ReportService : IReportService
{
    private readonly IAttendanceRepository _attendance;
    private readonly IOvertimeRepository _overtime;
    private readonly IEmployeeRepository _users;

    public ReportService(IAttendanceRepository attendance, IOvertimeRepository overtime, IEmployeeRepository users)
    {
        _attendance = attendance;
        _overtime = overtime;
        _users = users;
    }

    public async Task<IEnumerable<EmployeeHoursReport>> GetAllUsersHoursAsync()
    {
        var users = await _users.GetAllAsync();
        var result = new List<EmployeeHoursReport>();

        foreach (var user in users)
        {
            var records = await _attendance.GetByUserAsync(user.Id);
            var overtimes = await _overtime.GetByUserAsync(user.Id);

            result.Add(new EmployeeHoursReport
            {
                EmployeeId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Pozition = user.Pozition,
                TotalWorkedHours = records.Sum(r => r.WorkedHours),
                TotalOvertimeHours = overtimes.Sum(o => o.Hours)
            });
        }

        return result;
    }

    public async Task<PeriodReportResponse> GetUserPeriodReportAsync(PeriodReportRequest request)
    {
        var user = await _users.GetByIdAsync(request.UserId)
            ?? throw new KeyNotFoundException("Punonjësi nuk u gjet.");

        var records = await _attendance.GetByUserAndPeriodAsync(request.UserId, request.From, request.To);

        return new PeriodReportResponse
        {
            EmployeeId = user.Id,
            FullName = $"{user.FirstName} {user.LastName}",
            From = request.From,
            To = request.To,
            TotalHours = records.Sum(r => r.WorkedHours),
            DaysWorked = records.Select(r => r.Date.Date).Distinct().Count()
        };
    }

    public async Task<IEnumerable<OvertimeResponse>> GetAllOvertimeAsync()
    {
        var records = await _overtime.GetAllAsync();
        return records.Select(ToResponse);
    }

    public async Task<OvertimeResponse> AddOvertimeAsync(CreateOvertimeRequest request)
    {
        var user = await _users.GetByIdAsync(request.EmployeeId)
            ?? throw new KeyNotFoundException("Punonjësi nuk u gjet.");

        var overtime = new Overtime
        {
            EmployeeId = request.EmployeeId,
            Date = request.Date,
            StartTime = request.StartTime,
            Hours = request.Hours,
            Reason = request.Reason,
            OvertimeType = request.OvertimeType,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        var created = await _overtime.CreateAsync(overtime);
        created.Employee = user;
        return ToResponse(created);
    }

    private static OvertimeResponse ToResponse(Overtime o) => new()
    {
        Id = o.Id,
        EmployeeId = o.EmployeeId,
        FullName = $"{o.Employee.FirstName} {o.Employee.LastName}",
        Date = o.Date,
        Hours = o.Hours,
        Reason = o.Reason,
        Status = o.Status,
        OvertimeType = o.OvertimeType
    };
}