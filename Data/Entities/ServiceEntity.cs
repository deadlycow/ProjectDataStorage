using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class ServiceEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column(TypeName = "nvarchar(100)")]
  public string Name { get; set; } = null!;
  [Required]
  [Column(TypeName = "money")]
  public decimal Price { get; set; }

  public ICollection<ProjectEntity>? Project { get; set; }
}
