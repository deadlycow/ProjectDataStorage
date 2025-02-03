using Data.Entities;

namespace Business.Models;
public class PressentationDetailsModel
{
  public string ProjectNumber { get; set; } = null!;
  public string? Name { get; set; }
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public string? CustomerName { get; set; }
  public string? EmployeeName { get; set; }
  public string? Status { get; set; }
  public ICollection<string>? ServiceType { get; set; }
}
