using Business.Dto;
using Business.Models;
using Data.Entities;
using System.Text;
namespace Business.Factories;
public class ProjectFactory
{
  public static ProjectDto Create(ProjectEntity entity) => new()
  {
    Id = entity.Id,
    ProjectNumber = entity.ProjectNumber,
    Name = entity.Name,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    CustomerId = entity.CustomerId,
    EmployeeId = entity.EmployeeId,
    StatusTypeId = entity.StatusTypeId,
  };
  public static IEnumerable<ProjectDto> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create).ToList();

  public static ProjectDetails CreateDetails(ProjectEntity entity) => new()
  {
    Id = entity.Id,
    ProjectNumber = entity.ProjectNumber,
    ProjectName = entity.Name,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    CustomerId = entity.CustomerId,
    CustomerName = entity.Customer.Name,
    EmployeeId = entity.EmployeeId,
    EmployeeName = entity.Employees.Name,
    StatusTypeId = entity.StatusType.Id,
    StatusType = entity.StatusType.Name,
    Services = entity.ProjectService.Select(ps => new ServiceTypeDto
    {
      Id = ps.ServiceId,
      Name = ps.Services.Name,
      Price = ps.Services.Price,
    }).ToList(),
  };

  public static ProjectEntity Create(ProjectDto reg) => new()
  {
    Name = reg.Name,
    StartDate = reg.StartDate,
    EndDate = reg.EndDate,
    CustomerId = reg.CustomerId,
    EmployeeId = reg.EmployeeId,
    StatusTypeId = reg.StatusTypeId,
  };
  public static ProjectDto Update(ProjectDetails update) => new()
  {
    Id = update.Id,
    Name = update.ProjectName,
    StartDate = update.StartDate,
    EndDate = update.EndDate,
    CustomerId = update.CustomerId,
    EmployeeId = update.EmployeeId,
    StatusTypeId = update.StatusTypeId,
  };
  public static ProjectEntity Update(ProjectDto reg) => new()
  {
    Id = reg.Id,
    Name = reg.Name,
    StartDate = reg.StartDate,
    EndDate = reg.EndDate,
    CustomerId = reg.CustomerId,
    EmployeeId = reg.EmployeeId,
    StatusTypeId = reg.StatusTypeId,
  };
}
