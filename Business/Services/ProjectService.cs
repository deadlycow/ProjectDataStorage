using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services;
public class ProjectService(IProjectRepository repository) : IProjectService
{
  private readonly IProjectRepository _repository = repository;

  public async Task<IResult> CreateAsync(ProjectDto project)
  {
    var entity = ProjectFactory.Create(project);
    var response = await _repository.CreateAsync(entity);
    if (response)
      return Result.Ok();

    return Result.BadRequest("Error creating project");
  }
  public async Task<IResult> GetAsync(string projectNumber)
  {
    if (projectNumber == null)
      return Result.BadRequest("ID cannot be empty or whitespace");
    try
    {
      var response = await _repository.GetAsync(
          p => p.ProjectNumber == projectNumber,
          query => query
                .Include(p => p.Customer)
                .Include(p => p.Employees)
                .Include(p => p.StatusType)
                .Include(p => p.ProjectService)
                .ThenInclude(p => p.Services)
      );


      if (response == null)
        return Result.NotFound("Project not found");
      var project = ProjectFactory.CreateDetails(response);
      return Result<ProjectDetails>.Ok(project);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching project with ID{projectNumber}: {ex}");
      return Result.InternalServerError($"{ex}");
    }
  }
  public Task<IResult> UpdateAsync(string id, ProjectDto model)
  {  // dax att ta tag i denna 
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
    try
    {
      var respons = await _repository.GetAllAsync(
        query => query
        .Include(p => p.StatusType));

      if (respons == null || !respons.Any())
        return Result.NotFound("No projects");

      var projects = ProjectFactory.CreateList(respons);
      return Result<IEnumerable<ProjectDto>>.Ok(projects);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching all projects: {ex}");
      return Result.InternalServerError($"{ex}");
    }
  }
  public async Task<IResult> DeleteAsync(string projecNumber)
  {
    if (string.IsNullOrWhiteSpace(projecNumber))
      return Result.BadRequest("ID cannot be empty or whitespace");
    try
    {
      var response = await _repository.GetAsync(x => x.ProjectNumber == projecNumber);
      if (response == null)
        return Result.NotFound($"Project with ID {projecNumber} not found");

      var isDeleted = await _repository.DeleteAsync(response);
      if (!isDeleted)
        return Result.InternalServerError($"Failed to delete project with ID {projecNumber}");

      return Result.Ok();
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error deleting project {projecNumber}: {ex}");
      return Result.InternalServerError($"{ex}");
    }
  }
}