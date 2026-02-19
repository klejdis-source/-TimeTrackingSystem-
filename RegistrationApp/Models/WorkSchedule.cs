using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.Models;

/// Orari i punës (p.sh. 8:00-16:00, 9:00-17:00, etj.)

public class WorkSchedule
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Emri i orarit është i detyrueshëm")]
    [StringLength(100, ErrorMessage = "Emri nuk mund të jetë më shumë se 100 karaktere")]
    public string Name { get; set; } = string.Empty; // p.sh. "Orari Standard 8-16"

    [StringLength(500)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Ora e fillimit është e detyrueshme")]
    public TimeSpan StartTime { get; set; } // p.sh. 08:00:00

    [Required(ErrorMessage = "Ora e mbarimit është e detyrueshme")]
    public TimeSpan EndTime { get; set; } // p.sh. 16:00:00

    /// Sa orë duhet të punojë punonjësi sipas këtij orari (llogaritet automatikisht)

    public double ExpectedHours { get; set; } // p.sh. 8.0

    /// Ditët e javës kur aplikohet ky orar (p.sh. "Monday,Tuesday,Wednesday,Thursday,Friday")
    
    [StringLength(200)]
    public string? WorkDays { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property - Lista e punonjësve që punojnë me këtë orar
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
