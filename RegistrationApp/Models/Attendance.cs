using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Models;

public class Attendance
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Data është e detyrueshme")]
    public DateTime Date { get; set; } // Data e punës 

    [Required(ErrorMessage = "Ora e hyrjes është e detyrueshme")]
    public DateTime CheckInTime { get; set; } // Ora e saktë e hyrjes

    public DateTime? CheckOutTime { get; set; } // Ora e daljes 

    public double WorkedHours { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Present";

   
    public bool IsLate { get; set; }

    
    public int LateMinutes { get; set; }

   
    public bool IsEarlyLeave { get; set; }

    
    public int EarlyLeaveMinutes { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; } // Shënime për këtë ditë

    public bool IsApproved { get; set; } // A është aprovuar nga manageri

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public Employee Employee { get; set; } = null!;
}