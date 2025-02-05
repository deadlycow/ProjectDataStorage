using System.ComponentModel.DataAnnotations;

namespace Business.Dto;
public class ProjectDto
{
  public int Id { get; set; }
  public string ProjectNumber { get; set; } = null!;
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }

  public int CustomerId { get; set; }
  public string? Customer { get; set; }

  public int EmployeeId { get; set; }
  public string? Employee { get; set; }

  public int StatusTypeId { get; set; }
  public string? StatusTypeName { get; set; }
}
