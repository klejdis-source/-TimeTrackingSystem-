namespace TimeTrackingSystem.DTOs;

public class CreateOvertimeRequest
{
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public double Hours { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string OvertimeType { get; set; } = "Regular";
}