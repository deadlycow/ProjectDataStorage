﻿using Business.Dto;
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
    StatusTypeName = entity.StatusType.Name,
  };

  public static PressentationDetailsModel Fetch(ProjectEntity entity) => new()
  {
    ProjectNumber = entity.ProjectNumber,
    Name = entity.Name,
    StartDate = entity.StartDate,
    EndDate = entity.EndDate,
    CustomerName = entity.Customer.Name,
    EmployeeName = entity.Employees.Name,
    Status = entity.StatusType.Name,
    ServiceType = entity.ServiceTypes.Select(s => s.Name).ToList(),
  };

  public static IEnumerable<ProjectDto> CreateList(IEnumerable<ProjectEntity> entities) => entities.Select(Create).ToList();
  
}
