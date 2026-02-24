using TimeTrackingSystem.Models;

namespace TimeTrackingSystem.DTOs;
public class SignUpRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Pozition { get; set; } = string.Empty;
    //public string Role { get; set; } = "Employee";
    public Roles Roles { get; set; }

    public int? DepartmentId { get; set; }
    public int? OrariId { get; set; }
}