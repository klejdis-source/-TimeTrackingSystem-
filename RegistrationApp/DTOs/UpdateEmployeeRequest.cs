namespace TimeTrackingSystem.DTOs;

public class UpdateEmployeeRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Pozition { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public int OrariId { get; set; }
    public bool IsActive { get; set; }
}