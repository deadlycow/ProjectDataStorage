using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Dto;
public class ProjectDto
{
  public string ProjectNumber { get; set; } = null!;
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }

  public int CustomerId { get; set; }
  public CustomerEntity Customer { get; set; } = null!;

  public int EmployeeId { get; set; }
  public EmployeeEntity Employees { get; set; } = null!;

  public ICollection<ServiceEntity> ServiceTypes { get; set; } = null!;

  public int StatusTypeId { get; set; }
  public StatusEntity StatusType { get; set; } = null!;
}
