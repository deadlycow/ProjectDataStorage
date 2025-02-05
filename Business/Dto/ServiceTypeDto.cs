using System.ComponentModel.DataAnnotations;

namespace Business.Dto;
public class ServiceTypeDto
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public decimal Price { get; set; }
}
