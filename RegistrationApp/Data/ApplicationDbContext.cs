using TimeTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets - Tabelat në databazë
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Overtime> Overtimes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========== DEPARTMENT CONFIGURATION ==========
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name); // Index për search më të shpejtë
        });

        // ========== WORK SCHEDULE CONFIGURATION ==========
        modelBuilder.Entity<WorkSchedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.WorkDays).HasMaxLength(200);
        });

        // ========== EMPLOYEE CONFIGURATION ==========
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Position).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(20);

            // Unique constraint
            entity.HasIndex(e => e.Email).IsUnique();

            // Indexes për performance
            entity.HasIndex(e => e.DepartmentId);
            entity.HasIndex(e => e.WorkScheduleId);
            entity.HasIndex(e => e.IsActive);

            // Relationships

            // Employee -> Department (Many-to-One)
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull); // Nëse fshihet Department, DepartmentId bëhet null

            // Employee -> WorkSchedule (Many-to-One)
            entity.HasOne(e => e.WorkSchedule)
                .WithMany(ws => ws.Employees)
                .HasForeignKey(e => e.WorkScheduleId)
                .OnDelete(DeleteBehavior.SetNull); // Nëse fshihet WorkSchedule, WorkScheduleId bëhet null

            // Employee -> TimeEntries (One-to-Many)
            entity.HasMany(e => e.TimeEntries)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Nëse fshihet Employee, fshihen dhe TimeEntries

            // Employee -> Attendances (One-to-Many)
            entity.HasMany(e => e.Attendances)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee -> Overtimes (One-to-Many)
            entity.HasMany(e => e.Overtimes)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== TIME ENTRY CONFIGURATION ==========
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ClockInTime).IsRequired();
            entity.Property(e => e.IsActive).IsRequired();

            // Index për queries të shpeshta
            entity.HasIndex(e => new { e.EmployeeId, e.IsActive });
            entity.HasIndex(e => e.ClockInTime);
        });

        // ========== ATTENDANCE CONFIGURATION ==========
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.CheckInTime).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(500);

            // Unique constraint: Një punonjës mund të ketë vetëm një Attendance për çdo datë
            entity.HasIndex(e => new { e.EmployeeId, e.Date }).IsUnique();

            // Indexes
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.Status);
        });

        // ========== OVERTIME CONFIGURATION ==========
        modelBuilder.Entity<Overtime>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Hours).IsRequired();
            entity.Property(e => e.Reason).IsRequired().HasMaxLength(500);
            entity.Property(e => e.OvertimeType).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.ApprovalNotes).HasMaxLength(500);
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(18,2)");

            // Indexes
            entity.HasIndex(e => e.EmployeeId);
            entity.HasIndex(e => e.Date);
            entity.HasIndex(e => e.Status);
        });

        // ========== SEED DATA ==========

        // Departamentet
        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = 1,
                Name = "IT Department",
                Description = "Departamenti i Teknologjisë së Informacionit",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 2,
                Name = "HR Department",
                Description = "Departamenti i Burimeve Njerëzore",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = 3,
                Name = "Finance Department",
                Description = "Departamenti i Financës",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Oraret e Punës
        modelBuilder.Entity<WorkSchedule>().HasData(
            new WorkSchedule
            {
                Id = 1,
                Name = "Orari Standard 8-16",
                Description = "Orari standard i punës nga 8:00 deri në 16:00",
                StartTime = new TimeSpan(8, 0, 0),  // 08:00
                EndTime = new TimeSpan(16, 0, 0),   // 16:00
                ExpectedHours = 8.0,
                WorkDays = "Monday,Tuesday,Wednesday,Thursday,Friday",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new WorkSchedule
            {
                Id = 2,
                Name = "Orari Fleksibël 9-17",
                Description = "Orari fleksibël nga 9:00 deri në 17:00",
                StartTime = new TimeSpan(9, 0, 0),  // 09:00
                EndTime = new TimeSpan(17, 0, 0),   // 17:00
                ExpectedHours = 8.0,
                WorkDays = "Monday,Tuesday,Wednesday,Thursday,Friday",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new WorkSchedule
            {
                Id = 3,
                Name = "Orari i Natës 22-06",
                Description = "Orari i turnit të natës",
                StartTime = new TimeSpan(22, 0, 0), // 22:00
                EndTime = new TimeSpan(6, 0, 0),    // 06:00
                ExpectedHours = 8.0,
                WorkDays = "Monday,Tuesday,Wednesday,Thursday,Friday",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Admin User
        modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                Id = 1,
                DepartmentId = 1, // IT Department
                WorkScheduleId = 1, // Orari Standard
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@company.com",
                Position = "System Administrator",
                PhoneNumber = "+355692345678",
                IsActive = true,
                Role = "Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}