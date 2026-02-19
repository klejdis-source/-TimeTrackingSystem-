using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Models;
using TimeTrackingSystem.Repositories.Interfaces;

namespace TimeTrackingSystem.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;
    public EmployeeRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Employee>> GetAllAsync() =>
        await _db.Employees
            .Include(e => e.Department)
            .Include(e => e.Orari)
            .Where(e => e.IsActive)
            .ToListAsync();

    public async Task<Employee?> GetByIdAsync(int id) =>
        await _db.Employees
            .Include(e => e.Department)
            .Include(e => e.Orari)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Employee?> GetByEmailAsync(string email) =>
        await _db.Employees.FirstOrDefaultAsync(e => e.Email == email);

    public async Task<Employee> CreateAsync(Employee employee)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        _db.Employees.Update(employee);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var emp = await _db.Employees.FindAsync(id);
        if (emp is null) return false;
        emp.IsActive = false;
        await _db.SaveChangesAsync();
        return true;
    }
}