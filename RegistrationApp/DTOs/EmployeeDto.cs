using System.ComponentModel.DataAnnotations;
using TimeTrackingSystem.DTOs;

namespace TimeTrackingSystem.DTOs;

/// DTO për të krijuar një Employee të ri - 
/// 
public class CreateEmployeeDto
{
    [Required(ErrorMessage = "Emri është i detyrueshëm")]
    [StringLength(50, ErrorMessage = "Emri nuk mund të jetë më shumë se 50 karaktere")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mbiemri është i detyrueshëm")]
    [StringLength(50, ErrorMessage = "Mbiemri nuk mund të jetë më shumë se 50 karaktere")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email është i detyrueshëm")]
    [EmailAddress(ErrorMessage = "Formati i email-it nuk është valid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pozicioni është i detyrueshëm")]
    [StringLength(100, ErrorMessage = "Pozicioni nuk mund të jetë më shumë se 100 karaktere")]
    public string Position { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Formati i numrit të telefonit nuk është valid")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    // Relacione të reja
    public int? DepartmentId { get; set; }
    public int? WorkScheduleId { get; set; }

    public string? Password { get; set; }

    [StringLength(20)]
    public string Role { get; set; } = "Employee"; // "Employee", "Admin", "Manager"
}

/// DTO për të përditësuar një Employee ekzistues

public class UpdateEmployeeDto
{
    [Required(ErrorMessage = "Emri është i detyrueshëm")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mbiemri është i detyrueshëm")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email është i detyrueshëm")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pozicioni është i detyrueshëm")]
    [StringLength(100)]
    public string Position { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public int? DepartmentId { get; set; }
    public int? WorkScheduleId { get; set; }

    public string? Password { get; set; }

    [StringLength(20)]
    public string Role { get; set; } = "Employee";
}

/// DTO për të kthyer të dhënat e një Employee 

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }
    public string Role { get; set; } = string.Empty;

    // Relacione të reja
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public int? WorkScheduleId { get; set; }
    public string? WorkScheduleName { get; set; }
    public string? WorkScheduleTime { get; set; } // "08:00 - 16:00"

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// DTO me detaje të plota për një Employee
/// 
public class EmployeeDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }
    public string Role { get; set; } = string.Empty;

    // Department Info
    public int? DepartmentId { get; set; }
    public DepartmentDto? Department { get; set; }

    // Work Schedule Info
    public int? WorkScheduleId { get; set; }
    public WorkScheduleDto? WorkSchedule { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Statistika
    public double TotalWorkedHours { get; set; }
    public int TotalAttendances { get; set; }
    public int TotalOvertimeHours { get; set; }
    public double AttendanceRate { get; set; }
    public int LateDays { get; set; }
}

/// DTO për profile të punonjësit 

public class EmployeeProfileDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";

    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    public string Role { get; set; } = string.Empty;

    // Department & Schedule
    public string? DepartmentName { get; set; }
    public string? WorkScheduleName { get; set; }
    public TimeSpan? ScheduleStartTime { get; set; }
    public TimeSpan? ScheduleEndTime { get; set; }
    public double? ExpectedHours { get; set; }

    // Stats personale
    public double TotalWorkedHoursThisMonth { get; set; }
    public int AttendanceDaysThisMonth { get; set; }
    public int LateDaysThisMonth { get; set; }
    public double OvertimeHoursThisMonth { get; set; }

    // Recent activity
    public List<RecentAttendanceDto> RecentAttendances { get; set; } = new();
    public List<OvertimeListDto> PendingOvertimes { get; set; } = new();
}

public class RecentAttendanceDto
{
    public DateTime Date { get; set; }
    public string CheckIn { get; set; } = string.Empty;
    public string CheckOut { get; set; } = string.Empty;
    public double WorkedHours { get; set; }
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// DTO për dashboard të menaxherit
/// </summary>
public class ManagerDashboardDto
{
    // Team overview
    public int TotalTeamMembers { get; set; }
    public int ActiveMembers { get; set; }
    public int OnLeaveToday { get; set; }
    public int LateToday { get; set; }

    // Pending approvals
    public int PendingOvertimeRequests { get; set; }
    public int UnappprovedAttendances { get; set; }

    // This month stats
    public double TeamTotalHoursThisMonth { get; set; }
    public double TeamOvertimeHoursThisMonth { get; set; }
    public double TeamAttendanceRateThisMonth { get; set; }

    // Lists
    public List<EmployeeListDto> TeamMembers { get; set; } = new();
    public List<PendingOvertimeRequestDto> PendingOvertimes { get; set; } = new();
    public List<AttendanceListDto> TodayAttendances { get; set; } = new();
}

/// <summary>
/// DTO për admin dashboard
/// </summary>
public class AdminDashboardDto
{
    // Company-wide overview
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int TotalWorkSchedules { get; set; }

    // Today's stats
    public int PresentToday { get; set; }
    public int AbsentToday { get; set; }
    public int LateToday { get; set; }
    public int OnLeaveToday { get; set; }

    // This month
    public double TotalWorkedHoursThisMonth { get; set; }
    public double TotalOvertimeHoursThisMonth { get; set; }
    public double CompanyAttendanceRate { get; set; }

    // Pending items
    public int PendingOvertimes { get; set; }
    public int UnapprovedAttendances { get; set; }

    // Charts data
    public List<DepartmentStatsDto> DepartmentStats { get; set; } = new();
    public List<MonthlyTrendDto> MonthlyTrend { get; set; } = new();
}

public class DepartmentStatsDto
{
    public string DepartmentName { get; set; } = string.Empty;
    public int EmployeeCount { get; set; }
    public double TotalHours { get; set; }
    public double AttendanceRate { get; set; }
}

public class MonthlyTrendDto
{
    public string Month { get; set; } = string.Empty;
    public double TotalHours { get; set; }
    public double OvertimeHours { get; set; }
    public double AttendanceRate { get; set; }
}