namespace TimeTrackingSystem.DTOs;

public class EmployeeHoursReport
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Pozition { get; set; } = string.Empty;
    public double TotalWorkedHours { get; set; }
    public double TotalOvertimeHours { get; set; }
}