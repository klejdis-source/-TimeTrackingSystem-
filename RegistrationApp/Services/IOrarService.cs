using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Services;

public interface IOrarService
{
    Task<IEnumerable<WorkSchedule>> GetAllAsync();
    Task<WorkSchedule?> GetByIdAsync(int id);
    Task<WorkSchedule> CreateAsync(WorkSchedule orar);
    Task<WorkSchedule?> UpdateAsync(int id, WorkSchedule orar);
    Task<bool> DeleteAsync(int id);
}