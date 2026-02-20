using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _db;

    public DepartmentService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Department>> GetAllAsync() =>
        await _db.Departments
            .Include(d => d.Employees)
            .Where(d => d.IsActive)
            .ToListAsync();

    public async Task<Department?> GetByIdAsync(int id) =>
        await _db.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Department> CreateAsync(Department department)
    {
        department.CreatedAt = DateTime.UtcNow;
        _db.Departments.Add(department);
        await _db.SaveChangesAsync();
        return department;
    }

    public async Task<Department?> UpdateAsync(int id, Department department)
    {
        var existing = await _db.Departments.FindAsync(id);
        if (existing is null) return null;

        existing.Name = department.Name;
        existing.Description = department.Description;
        existing.IsActive = department.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Departments.FindAsync(id);
        if (existing is null) return false;

        existing.IsActive = false;
        existing.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }
}