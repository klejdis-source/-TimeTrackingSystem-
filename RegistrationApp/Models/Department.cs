using System.ComponentModel.DataAnnotations;

namespace TimeTrackingSystem.Models;
public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Emri i departamentit është i detyrueshëm")]
    [StringLength(100, ErrorMessage = "Emri nuk mund të jetë më shumë se 100 karaktere")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere")]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation property - Lista e punonjësve në këtë departament
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
