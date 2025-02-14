using System.ComponentModel.DataAnnotations;

namespace Business.Dto;
public class ProjectDto
{
  public int Id { get; set; }
  public string? ProjectNumber { get; set; }
  [Required(ErrorMessage = "Projektnamn är obligatoriskt.")]
  public string Name { get; set; } = null!;
  [Required(ErrorMessage = "Start datum är obligatoriskt.")]
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }

  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "En kund är obligatorisk")]
  public int CustomerId { get; set; }
  public string? Customer { get; set; }

  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "En anställd är obligatorisk")]
  public int EmployeeId { get; set; }
  public string? Employee { get; set; }

  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "En status är obligatorisk")]
  public int StatusTypeId { get; set; }
  public string? StatusTypeName { get; set; }
}
