using System.ComponentModel.DataAnnotations;

namespace Business.Models;
public class PressentationModel
{
  public string? ProjectNumber { get; set; }
  public string? Name { get; set; }
  public DateOnly StartDate { get; set; }
  public DateOnly? EndDate { get; set; }
  public string? CustomerName { get; set; }
  public string? EmployeeName { get; set; }
  public string? Status { get; set; }

}
