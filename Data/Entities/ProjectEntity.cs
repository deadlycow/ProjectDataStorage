using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class ProjectEntity
{
  [Key]
  public int Id { get; set; }
  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  public string ProjectNumber { get; set; } = null!;
  [Required]
  [Column(TypeName = "nvarchar(50)")]
  public string Name { get; set; } = null!;
  [Required]
  [Column(TypeName = "date")]
  public DateOnly StartDate { get; set; }
  [Column(TypeName = "date")]
  public DateOnly? EndDate { get; set; }
  
  public int CustomerId { get; set; }
  public CustomerEntity Customer { get; set; } = null!;
  public int EmployeeId { get; set; }
  public EmployeeEntity Employees { get; set; } = null!;
  public ICollection<ServiceEntity> ServiceTypes { get; set; } = null!;
  public int StatusTypeId { get; set; }
  public StatusEntity StatusType { get; set; } = null!;
}
