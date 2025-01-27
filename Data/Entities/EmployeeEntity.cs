using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class EmployeeEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(100)")]
  public string Name { get; set; } = null!;

  public ICollection<ProjectEntity>? Project { get; set; }
}
