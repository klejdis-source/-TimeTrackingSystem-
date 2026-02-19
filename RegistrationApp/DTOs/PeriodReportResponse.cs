namespace TimeTrackingSystem.DTOs;

public class PeriodReportResponse
{
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public double TotalHours { get; set; }
    public int DaysWorked { get; set; }
}