namespace TimeTrackingSystem.DTOs;

public class PeriodReportRequest
{
    public int UserId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}