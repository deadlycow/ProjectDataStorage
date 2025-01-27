using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;
public class StatusEntity
{
  [Key]
  public int Id { get; set; }
  [Required]
  [Column("nvarchar(100)")]
  public string Status { get; set; } = null!;

  public ICollection<ProjectEntity> Project { get; set; } = null!;
}
