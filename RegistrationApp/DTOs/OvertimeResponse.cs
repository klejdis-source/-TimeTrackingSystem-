namespace TimeTrackingSystem.DTOs;

public class OvertimeResponse
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string OvertimeType { get; set; } = string.Empty;
}