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
            Roles = Roles.Admin_Access,
            PasswordHash = "$2a$11$RJDla1K73tLW9St81vvftuKmpEXprsebCENr9m6js8T.slt/3zKNO",// te sistemohet 
            CreatedAt = new DateTime(2024, 1, 1),
            DepartmentId = null,
            OrariId = null
        });
    }
}
