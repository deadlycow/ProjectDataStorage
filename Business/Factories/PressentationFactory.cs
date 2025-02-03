using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Net.Http.Headers;

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
      Status = project.StatusType.Name
    };
  }
  public static PressentationDetailsModel Fetch(ProjectEntity project)
  {
    return new()
    {
      ProjectNumber = project.ProjectNumber,
      Name = project.Name,
      StartDate = project.StartDate,
      EndDate = project.EndDate,
      CustomerName = project.Customer.Name,
      EmployeeName = project.Employees.Name,
      Status = project.StatusType.Name,
      ServiceType = project.ServiceTypes.Select(s => s.Name).ToList()
    };
  }
}
