using System.ComponentModel.DataAnnotations;

namespace TimeTrackingSystem.DTOs;

/// DTO për të regjistruar një Attendance të ri (Check In)

public class CreateAttendanceDto
{
    [Required(ErrorMessage = "ID e punonjësit është e detyrueshme")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Data është e detyrueshme")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Ora e hyrjes është e detyrueshme")]
    public DateTime CheckInTime { get; set; }

    [StringLength(500, ErrorMessage = "Shënimet nuk mund të jenë më shumë se 500 karaktere")]
    public string? Notes { get; set; }
}

/// DTO për të bërë Check Out

public class CheckOutAttendanceDto
{
    [Required(ErrorMessage = "ID e punonjësit është e detyrueshme")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Ora e daljes është e detyrueshme")]
    public DateTime CheckOutTime { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

/// DTO për të përditësuar një Attendance ekzistues

public class UpdateAttendanceDto
{
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    [StringLength(20)]
    public string? Status { get; set; } // "Present", "Absent", "Late", "OnLeave", "Sick"

    [StringLength(500)]
    public string? Notes { get; set; }
}

/// DTO për aprovim nga menaxheri

public class ApproveAttendanceDto
{
    [Required(ErrorMessage = "ID e attendance është e detyrueshme")]
    public int AttendanceId { get; set; }

    [Required(ErrorMessage = "Statusi i aprovimit është i detyrueshëm")]
    public bool IsApproved { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

/// DTO për të kthyer të dhënat e një Attendance

public class AttendanceDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeePosition { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }

    public double WorkedHours { get; set; }
    public string Status { get; set; } = string.Empty;

    public bool IsLate { get; set; }
    public int LateMinutes { get; set; }
    public bool IsEarlyLeave { get; set; }
    public int EarlyLeaveMinutes { get; set; }

    public string? Notes { get; set; }
    public bool IsApproved { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Properties të formatuara për display
    public string DateFormatted => Date.ToString("dd/MM/yyyy");
    public string CheckInFormatted => CheckInTime.ToString("HH:mm");
    public string CheckOutFormatted => CheckOutTime?.ToString("HH:mm") ?? "N/A";
    public string StatusDisplay => GetStatusDisplay();

    private string GetStatusDisplay()
    {
        return Status switch
        {
            "Present" => "Prezent",
            "Late" => "Me Vonesë",
            "Absent" => "Mungon",
            "OnLeave" => "Në Pushim",
            "Sick" => "I Sëmurë",
            _ => Status
        };
    }
}

/// DTO për listim të thjeshtë

public class AttendanceListDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string CheckInTime { get; set; } = string.Empty;
    public string CheckOutTime { get; set; } = string.Empty;
    public double WorkedHours { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsLate { get; set; }
    public bool IsApproved { get; set; }
}

/// DTO për raport ditor të prezencës

public class DailyAttendanceReportDto
{
    public DateTime Date { get; set; }
    public int TotalEmployees { get; set; }
    public int PresentEmployees { get; set; }
    public int AbsentEmployees { get; set; }
    public int LateEmployees { get; set; }
    public int OnLeaveEmployees { get; set; }

    public List<AttendanceListDto> Attendances { get; set; } = new();

    // Statistika
    public double AttendanceRate => TotalEmployees > 0
        ? Math.Round((double)PresentEmployees / TotalEmployees * 100, 2)
        : 0;

    public double LateRate => PresentEmployees > 0
        ? Math.Round((double)LateEmployees / PresentEmployees * 100, 2)
        : 0;
}

/// DTO për raport periodik të prezencës

public class AttendancePeriodReportDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Statistika
    public int TotalDays { get; set; }
    public int PresentDays { get; set; }
    public int AbsentDays { get; set; }
    public int LateDays { get; set; }
    public int LeaveDays { get; set; }

    public double TotalWorkedHours { get; set; }
    public double AverageWorkedHours { get; set; }

    // Detaje
    public List<AttendanceDto> Attendances { get; set; } = new();

    // Rates
    public double AttendanceRate => TotalDays > 0
        ? Math.Round((double)PresentDays / TotalDays * 100, 2)
        : 0;
}

/// DTO për bulk import të attendances

public class BulkAttendanceDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public List<EmployeeAttendanceDto> Employees { get; set; } = new();
}

public class EmployeeAttendanceDto
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public DateTime CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Present";

    public string? Notes { get; set; }
}