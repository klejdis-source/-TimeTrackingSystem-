using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Models;


public class Overtime
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Data është e detyrueshme")]
    public DateTime Date { get; set; } // Data kur janë bërë orët shtesë

    public DateTime StartTime { get; set; } // Ora e fillimit të overtime
    public DateTime? EndTime { get; set; } // Ora e mbarimit (null nëse vazhdon)

    [Required(ErrorMessage = "Orët janë të detyrueshme")]
    [Range(0.1, 24, ErrorMessage = "Orët duhet të jenë midis 0.1 dhe 24")]
    public double Hours { get; set; } // Sa orë shtesë

    [Required(ErrorMessage = "Arsyeja është e detyrueshme")]
    [StringLength(500, ErrorMessage = "Arsyeja nuk mund të jetë më shumë se 500 karaktere")]
    public string Reason { get; set; } = string.Empty; // Arsyeja e overtime

    
    [StringLength(20)]
    public string OvertimeType { get; set; } = "Regular";

    
    [StringLength(20)]
    public string Status { get; set; } = "Pending";

    
    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    [StringLength(500)]
    public string? ApprovalNotes { get; set; } // komente nga menaxheri

   
    public bool IsPaid { get; set; } = false;

    public DateTime? PaidAt { get; set; }

    
    public decimal? PaymentAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public Employee Employee { get; set; } = null!;
}
