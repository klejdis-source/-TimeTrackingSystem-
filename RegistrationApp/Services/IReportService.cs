using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.Services;

public interface IReportService
{
    Task<IEnumerable<EmployeeHoursReport>> GetAllUsersHoursAsync();
    Task<PeriodReportResponse> GetUserPeriodReportAsync(PeriodReportRequest request);
    Task<IEnumerable<OvertimeResponse>> GetAllOvertimeAsync();
    Task<OvertimeResponse> AddOvertimeAsync(CreateOvertimeRequest request);
}