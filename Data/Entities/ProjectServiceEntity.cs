namespace Data.Entities;
public class ProjectServiceEntity
{
  public int ProjectId { get; set; }
  public ProjectEntity Projects { get; set; } = null!;
  public int ServiceId { get; set; }
  public ServiceEntity Services { get; set; } = null!;
}
