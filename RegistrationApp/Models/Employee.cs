using System.ComponentModel.DataAnnotations;

namespace TimeTrackingSystem.Models;
public class Employee
{
    public int Id { get; set; }

    // Foreign Keys
    public int? DepartmentId { get; set; } // Nullable - një punonjës mund të mos ketë departament
    public int? WorkScheduleId { get; set; } // Nullable - mund të mos ketë orar të caktuar

    [Required(ErrorMessage = "Emri është i detyrueshëm")]
    [StringLength(50, ErrorMessage = "Emri nuk mund të jetë më shumë se 50 karaktere")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mbiemri është i detyrueshëm")]
    [StringLength(50, ErrorMessage = "Mbiemri nuk mund të jetë më shumë se 50 karaktere")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email është i detyrueshëm")]
    [EmailAddress(ErrorMessage = "Formati i email-it nuk është valid")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pozicioni është i detyrueshëm")]
    [StringLength(100, ErrorMessage = "Pozicioni nuk mund të jetë më shumë se 100 karaktere")]
    public string Position { get; set; } = string.Empty;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(500)]
    public string? PasswordHash { get; set; }

    [Required]
    [StringLength(20)]
    public string Role { get; set; } = "Employee"; // Employee, Admin, Manager

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public Department? Department { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }

    // Collections
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Overtime> Overtimes { get; set; } = new List<Overtime>();
}