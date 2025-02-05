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
    StatusTypeName = entity.StatusType.Name,
  };
  public static IEnumerable<ProjectDto> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create).ToList();

  public static ProjectDetails CreateDetails(ProjectEntity entity) => new()
  {
    Id = entity.Id,
    ProjectNumber = entity.ProjectNumber,
    ProjectName = entity.Name,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    CustomerName = entity.Customer.Name,
    EmployeeName = entity.Employees.Name,
    StatusType = entity.StatusType.Name,
    Services = entity.ServiceTypes.Select(x => x.Name).ToList(),
  };
}
