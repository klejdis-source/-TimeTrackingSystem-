namespace TimeTrackingSystem.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Pozition { get; set; } = string.Empty;
    public string Role { get; set; } = "Employee"; // Employee | Admin
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int DepartmentId { get; set; }
    public int OrariId { get; set; }

    public Department? Department { get; set; }
    public WorkSchedule? Orari { get; set; }

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Overtime> Overtimes { get; set; } = new List<Overtime>();
}
