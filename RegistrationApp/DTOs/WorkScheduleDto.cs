using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.DTOs;

/// DTO për të krijuar një WorkSchedule të ri

public class CreateWorkScheduleDto
{
    [Required(ErrorMessage = "Emri i orarit është i detyrueshëm")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Emri duhet të jetë midis 3 dhe 100 karakteresh")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Ora e fillimit është e detyrueshme")]
    public TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "Ora e mbarimit është e detyrueshme")]
    public TimeSpan EndTime { get; set; }

    [StringLength(200, ErrorMessage = "Ditët e punës nuk mund të jenë më shumë se 200 karaktere")]
    public string? WorkDays { get; set; } // "Monday,Tuesday,Wednesday,Thursday,Friday"

    public bool IsActive { get; set; } = true;
}

/// DTO për të përditësuar një WorkSchedule ekzistues

public class UpdateWorkScheduleDto
{
    [Required(ErrorMessage = "Emri i orarit është i detyrueshëm")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Emri duhet të jetë midis 3 dhe 100 karakteresh")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Ora e fillimit është e detyrueshme")]
    public TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "Ora e mbarimit është e detyrueshme")]
    public TimeSpan EndTime { get; set; }

    [StringLength(200)]
    public string? WorkDays { get; set; }

    public bool IsActive { get; set; }
}

/// DTO për të kthyer të dhënat e një WorkSchedule

public class WorkScheduleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double ExpectedHours { get; set; }

    public string? WorkDays { get; set; }
    public List<string> WorkDaysList { get; set; } = new(); // Array i ditëve: ["Monday", "Tuesday", ...]

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Statistika
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }

    // Format të lexueshëm
    public string StartTimeFormatted => StartTime.ToString(@"hh\:mm");  // "08:00"
    public string EndTimeFormatted => EndTime.ToString(@"hh\:mm");      // "16:00"
    public string ScheduleDisplay => $"{StartTimeFormatted} - {EndTimeFormatted}"; // "08:00 - 16:00"
}

/// DTO për listim të thjeshtë

public class WorkScheduleListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ScheduleDisplay { get; set; } = string.Empty; // "08:00 - 16:00"
    public double ExpectedHours { get; set; }
    public bool IsActive { get; set; }
    public int EmployeeCount { get; set; }
}

/// DTO me detaje të plota
/// 
public class WorkScheduleDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double ExpectedHours { get; set; }

    public string? WorkDays { get; set; }
    public List<string> WorkDaysList { get; set; } = new();

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // Lista e punonjësve
    public List<EmployeeListDto> Employees { get; set; } = new();

    // Statistika
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public double AverageWorkingHours { get; set; }  // Mesatarja e orëve të punuara
}