using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponse>> GetAllAsync();
    Task<EmployeeResponse?> GetByIdAsync(int id);
    Task<EmployeeResponse> CreateAsync(CreateEmployeeRequest request);
    Task<EmployeeResponse?> UpdateAsync(int id, UpdateEmployeeRequest request);
    Task<bool> DeactivateAsync(int id);
}