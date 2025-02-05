namespace Business.Models;
public class ProjectDetails
{
  public int Id { get; set; }
  public string ProjectNumber { get; set; } = null!;
  public string ProjectName { get; set; } = null!;
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public string? CustomerName { get; set; }
  public string? EmployeeName { get; set; }
  public string StatusType { get; set; } = null!;
  public ICollection<string>? Services { get; set; }
}
