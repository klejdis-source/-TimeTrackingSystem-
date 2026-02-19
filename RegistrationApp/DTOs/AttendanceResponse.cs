namespace TimeTrackingSystem.DTOs;

public class AttendanceResponse
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public double WorkedHours { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool IsLate { get; set; }
    public int LateMinutes { get; set; }
}