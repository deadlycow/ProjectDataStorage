using System.ComponentModel.DataAnnotations;

namespace Business.Dto;
public class ProjectDto
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string ProjectNumber { get; set; } = null!;
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  [Required]
  public int CustomerId { get; set; }
  [Required]
  public int EmployeeId { get; set; }
  [Required]
  public int StatusTypeId { get; set; }
}
