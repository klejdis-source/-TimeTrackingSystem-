using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Models;

public class Attendance
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public DateTime ClockIn { get; set; }

    public DateTime? ClockOut { get; set; }

    public double WorkedHours { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Present";

    public bool IsLate { get; set; }

    public int LateMinutes { get; set; }

    public bool IsEarlyLeave { get; set; }

    public int EarlyLeaveMinutes { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public bool IsApproved { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public Employee Employee { get; set; } = null!;
}