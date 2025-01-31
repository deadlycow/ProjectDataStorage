using Business.Dto;
using Business.Models;
using Data.Entities;

namespace Business.Factories;
public static class PressentationFactory
{
  public static PressentationModel Create() => new();
  public static PressentationModel Create(ProjectEntity project)
  {
    return new()
    {
      ProjectNumber = project.ProjectNumber,
      Name = project.Name,
      StartDate = project.StartDate,
      EndDate = project.EndDate,
      CustomerName = project.Customer.Name,
      EmployeeName = project.Employees.Name,
      Status = project.StatusType.Name
    };
  }
}
