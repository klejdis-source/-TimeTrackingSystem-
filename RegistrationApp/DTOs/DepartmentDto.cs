using System.ComponentModel.DataAnnotations;

namespace TimeTrackingSystem.DTOs;

/// DTO për të krijuar një Department të ri

public class CreateDepartmentDto
{
    [Required(ErrorMessage = "Emri i departamentit është i detyrueshëm")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Emri duhet të jetë midis 2 dhe 100 karakteresh")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere")]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

/// DTO për të përditësuar një Department ekzistues

public class UpdateDepartmentDto
{
    [Required(ErrorMessage = "Emri i departamentit është i detyrueshëm")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Emri duhet të jetë midis 2 dhe 100 karakteresh")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Përshkrimi nuk mund të jetë më shumë se 500 karaktere")]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}

/// DTO për të kthyer të dhënat e një Department

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Statistika
    public int TotalEmployees { get; set; }      // Sa punonjës ka në total
    public int ActiveEmployees { get; set; }     // Sa punonjës aktivë ka
}

/// DTO për listim të thjeshtë 

public class DepartmentListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int EmployeeCount { get; set; }
}

/// DTO me detaje të plota (përfshirë listën e punonjësve)

public class DepartmentDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // Lista e punonjësve
    public List<EmployeeListDto> Employees { get; set; } = new();

    // Statistika
    public int TotalEmployees { get; set; }
    public int ActiveEmployees { get; set; }
    public double AverageTenure { get; set; }    // Mesatarja e kohëzgjatjes së punës
}

/// DTO për punonjësin në listën e departamentit
/// 
public class EmployeeListDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}