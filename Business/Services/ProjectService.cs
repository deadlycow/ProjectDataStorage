using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Services;
public class ProjectService(IProjectRepository repository) : IProjectService
{
  private readonly IProjectRepository _repository = repository;

  public Task<IResult> CreateAsync(ProjectDto project)
  {
    throw new NotImplementedException();
  }
  public async Task<IResult> GetByExpressionAsync(string projectNumber)
  {
    var project = await _repository.GetAsync(
        p => p.ProjectNumber == projectNumber,
        query => query
              .Include(p => p.Customer)
              .Include(p => p.Employees)
              .Include(p => p.StatusType)
              .Include(p => p.ServiceTypes)
    );

    if (project == null)
      return Result.NotFound("Project not found");

    var model = ProjectFactory.Fetch(project);

    return Result<PressentationDetailsModel>.Ok(model);
  }
  public async Task<IResult> UpdateAsync(string projectNumber, PressentationDetailsModel model)
  {
    throw new NotImplementedException();
    //var project = await _repository.GetAsync(
    //    p => p.ProjectNumber == projectNumber,
    //    query => query
    //          .Include(p => p.Customer)
    //          .Include(p => p.Employees)
    //          .Include(p => p.StatusType)
    //          .Include(p => p.ServiceTypes)
    //);

    //project.Name = model.Name;
    //project.StartDate = model.StartDate;
    //project.EndDate = model.EndDate;
    //project.Customer.Name = model.CustomerName;
    //project.Employees.Name = model.EmployeeName;
    //project.StatusType.Name = model.Status;
    //project.ServiceTypes = model.ServiceType?.Select(st => new ServiceType { Name = st }).ToList();



    //var response = await _repository.UpdateAsync(project);
    //if (response)
    //  return Result.Ok();

    //return Result.BadRequest("Something went wrong");
  }
  public async Task<IResult> GetAllAsync()
  {
    var projectEntity = await _repository.GetAllAsync();

    var projects = projectEntity?.Select(PressentationFactory.Create);

    return Result<IEnumerable<PressentationModel>>.Ok(projects);
  }
  public Task<IResult> DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }
}
