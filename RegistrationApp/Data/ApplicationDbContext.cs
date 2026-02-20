using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<WorkSchedule> Orars => Set<WorkSchedule>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Overtime> Overtimes => Set<Overtime>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();

            e.HasOne(u => u.Department)
             .WithMany(d => d.Employees)
             .HasForeignKey(u => u.DepartmentId);

            e.HasOne(u => u.Orari)
             .WithMany(o => o.Employees)
             .HasForeignKey(u => u.OrariId);
        });

        modelBuilder.Entity<Attendance>(e =>
        {
            e.HasOne(a => a.Employee)
             .WithMany(u => u.Attendances)
             .HasForeignKey(a => a.EmployeeId);
        });

        modelBuilder.Entity<Overtime>(e =>
        {
            e.HasOne(o => o.Employee)
             .WithMany(u => u.Overtimes)
             .HasForeignKey(o => o.EmployeeId);
        });

        // Seed admin user
        modelBuilder.Entity<Employee>().HasData(new Employee
        {
            Id = 1,
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@company.com",
            Pozition = "System Administrator",
            IsActive = true,
            Role = "Admin",
            // Përdor një hash të gatshëm (ky më poshtë është hash-i i "Admin123!")
            PasswordHash = "$2a$11$ev7LzH.6Yf3DovD29.7G9uRPhYpUfH6r7G.RjXGqY5U1K/6Xh/vG.",
            CreatedAt = new DateTime(2024, 1, 1),
            DepartmentId = null,
            OrariId =null
        });
    }
}
