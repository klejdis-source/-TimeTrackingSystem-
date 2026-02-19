using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Repositories.Interfaces;

public interface IOvertimeRepository
{
    Task<IEnumerable<Overtime>> GetAllAsync();
    Task<IEnumerable<Overtime>> GetByUserAsync(int userId);
    Task<Overtime> CreateAsync(Overtime overtime);
}