using Business.Dto;
using Business.Models;
using Data.Entities;
namespace Business.Factories;
public class ProjectFactory
{
  public static ProjectDto Create() => new();
  public static ProjectDto Create(ProjectEntity entity) => new()
  {
    Id = entity.Id,
    ProjectNumber = entity.ProjectNumber,
    Name = entity.Name,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    EmployeeId = entity.EmployeeId,
    CustomerId = entity.CustomerId,
    StatusTypeId = entity.StatusTypeId
  };
  public static ProjectEntity Create(ProjectDto form) => new()
  {
    Name = form.Name,
    StartDate = form.StartDate,
    EndDate = form.EndDate,
    EmployeeId = form.EmployeeId,
    CustomerId = form.CustomerId,
    StatusTypeId = form.StatusTypeId
  };
}
