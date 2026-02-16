using System.ComponentModel.DataAnnotations;

namespace TimeTrackingSystem.DTOs;

/// DTO për të krijuar një kërkesë Overtime

public class CreateOvertimeDto
{
    [Required(ErrorMessage = "ID e punonjësit është e detyrueshme")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Data është e detyrueshme")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Ora e fillimit është e detyrueshme")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Ora e mbarimit është e detyrueshme")]
    public DateTime EndTime { get; set; }

    [Required(ErrorMessage = "Arsyeja është e detyrueshme")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Arsyeja duhet të jetë midis 10 dhe 500 karakteresh")]
    public string Reason { get; set; } = string.Empty;

    [StringLength(20)]
    public string OvertimeType { get; set; } = "Regular"; // "Regular", "Holiday", "Weekend", "Emergency"
}

/// DTO për të përditësuar një Overtime ekzistues (para aprovimit)
/// 
public class UpdateOvertimeDto
{
    public DateTime? Date { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    [StringLength(500, MinimumLength = 10)]
    public string? Reason { get; set; }

    [StringLength(20)]
    public string? OvertimeType { get; set; }
}

/// DTO për aprovim/refuzim nga menaxheri

public class ApproveOvertimeDto
{
    [Required(ErrorMessage = "ID e overtime është e detyrueshme")]
    public int OvertimeId { get; set; }

    [Required(ErrorMessage = "Statusi është i detyrueshëm")]
    [RegularExpression("^(Approved|Rejected)$", ErrorMessage = "Statusi duhet të jetë 'Approved' ose 'Rejected'")]
    public string Status { get; set; } = string.Empty; // "Approved" ose "Rejected"

    [Required(ErrorMessage = "ID e aprovuesit është e detyrueshme")]
    public int ApprovedBy { get; set; }

    [StringLength(500, ErrorMessage = "Shënimet nuk mund të jenë më shumë se 500 karaktere")]
    public string? ApprovalNotes { get; set; }
}

/// DTO për të shënuar si të paguar
/// 
public class MarkOvertimeAsPaidDto
{
    [Required(ErrorMessage = "ID e overtime është e detyrueshme")]
    public int OvertimeId { get; set; }

    [Required(ErrorMessage = "Shuma e pagesës është e detyrueshme")]
    [Range(0.01, 1000000, ErrorMessage = "Shuma duhet të jetë midis 0.01 dhe 1,000,000")]
    public decimal PaymentAmount { get; set; }

    public DateTime? PaidAt { get; set; }
}

/// DTO për bulk approval

public class BulkApproveOvertimeDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Duhet të keni të paktën një overtime për aprovim")]
    public List<int> OvertimeIds { get; set; } = new();

    [Required]
    [RegularExpression("^(Approved|Rejected)$")]
    public string Status { get; set; } = string.Empty;

    [Required]
    public int ApprovedBy { get; set; }

    [StringLength(500)]
    public string? ApprovalNotes { get; set; }
}

/// DTO për të kthyer të dhënat e një Overtime

public class OvertimeDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeePosition { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public double Hours { get; set; }

    public string Reason { get; set; } = string.Empty;
    public string OvertimeType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public int? ApprovedBy { get; set; }
    public string? ApproverName { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovalNotes { get; set; }

    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
    public decimal? PaymentAmount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Properties të formatuara
    public string DateFormatted => Date.ToString("dd/MM/yyyy");
    public string StartTimeFormatted => StartTime.ToString("HH:mm");
    public string EndTimeFormatted => EndTime?.ToString("HH:mm") ?? "N/A";
    public string StatusDisplay => GetStatusDisplay();
    public string OvertimeTypeDisplay => GetOvertimeTypeDisplay();

    private string GetStatusDisplay()
    {
        return Status switch
        {
            "Pending" => "Në Pritje",
            "Approved" => "Aprovuar",
            "Rejected" => "Refuzuar",
            _ => Status
        };
    }

    private string GetOvertimeTypeDisplay()
    {
        return OvertimeType switch
        {
            "Regular" => "Normale",
            "Holiday" => "Festë",
            "Weekend" => "Fundjavë",
            "Emergency" => "Urgjencë",
            _ => OvertimeType
        };
    }
}

/// DTO për listim të thjeshtë

public class OvertimeListDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public string OvertimeType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
}

/// DTO për raport të overtime-ve për një punonjës

public class EmployeeOvertimeReportDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Statistika
    public double TotalOvertimeHours { get; set; }
    public int TotalOvertimeRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int PendingRequests { get; set; }
    public int RejectedRequests { get; set; }

    public decimal TotalPaymentAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal UnpaidAmount { get; set; }

    // Breakdown sipas llojit
    public double RegularHours { get; set; }
    public double HolidayHours { get; set; }
    public double WeekendHours { get; set; }
    public double EmergencyHours { get; set; }

    // Detaje
    public List<OvertimeDto> Overtimes { get; set; } = new();
}

/// DTO për raport të përgjithshëm të overtime-ve

public class OvertimeSummaryReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Statistika të përgjithshme
    public double TotalOvertimeHours { get; set; }
    public int TotalRequests { get; set; }
    public int ApprovedRequests { get; set; }
    public int PendingRequests { get; set; }
    public int RejectedRequests { get; set; }

    public decimal TotalPaymentAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal UnpaidAmount { get; set; }

    // Top punonjësit me më shumë overtime
    public List<TopOvertimeEmployeeDto> TopEmployees { get; set; } = new();

    // Breakdown sipas departamentit
    public List<DepartmentOvertimeDto> DepartmentBreakdown { get; set; } = new();

    // Trend mujor
    public List<MonthlyOvertimeDto> MonthlyTrend { get; set; } = new();
}

public class TopOvertimeEmployeeDto
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public double TotalHours { get; set; }
    public int RequestCount { get; set; }
    public decimal TotalPayment { get; set; }
}

public class DepartmentOvertimeDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public double TotalHours { get; set; }
    public int EmployeeCount { get; set; }
    public decimal TotalPayment { get; set; }
}

public class MonthlyOvertimeDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; } = string.Empty;
    public double TotalHours { get; set; }
    public int RequestCount { get; set; }
    public decimal TotalPayment { get; set; }
}

/// DTO për pending overtime requests (për menaxherë)
/// 
public class PendingOvertimeRequestDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string OvertimeType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int DaysSinceRequest { get; set; }
}