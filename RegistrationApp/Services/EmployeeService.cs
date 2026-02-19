using TimeTrackingSystem.DTOs;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
    {
        var employees = await _repo.GetAllAsync();
        return employees.Select(ToResponse);
    }

    public async Task<EmployeeResponse?> GetByIdAsync(int id)
    {
        var emp = await _repo.GetByIdAsync(id);
        return emp is null ? null : ToResponse(emp);
    }

    public async Task<EmployeeResponse> CreateAsync(CreateEmployeeRequest request)
    {
        var existing = await _repo.GetByEmailAsync(request.Email);
        if (existing is not null)
            throw new InvalidOperationException("Një punonjës me këtë email ekziston.");

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Pozition = request.Pozition,
            Role = request.Role,
            DepartmentId = request.DepartmentId,
            OrariId = request.OrariId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repo.CreateAsync(employee);
        var full = await _repo.GetByIdAsync(created.Id);
        return ToResponse(full!);
    }

    public async Task<EmployeeResponse?> UpdateAsync(int id, UpdateEmployeeRequest request)
    {
        var emp = await _repo.GetByIdAsync(id);
        if (emp is null) return null;

        emp.FirstName = request.FirstName;
        emp.LastName = request.LastName;
        emp.Pozition = request.Pozition;
        emp.DepartmentId = request.DepartmentId;
        emp.OrariId = request.OrariId;
        emp.IsActive = request.IsActive;

        await _repo.UpdateAsync(emp);
        var updated = await _repo.GetByIdAsync(id);
        return ToResponse(updated!);
    }

    public async Task<bool> DeactivateAsync(int id) =>
        await _repo.DeactivateAsync(id);

    private static EmployeeResponse ToResponse(Employee e) => new()
    {
        Id = e.Id,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        Pozition = e.Pozition,
        Role = e.Role,
        IsActive = e.IsActive,
        DepartmentId = e.DepartmentId,
        DepartmentName = e.Department?.Name,
        OrariId = e.OrariId,
        OrariName = e.Orari?.Name
    };
}