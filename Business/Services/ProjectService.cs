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
    if (project == null)
      return Result.BadRequest("Project is null");
    try
    {
      var entity = ProjectFactory.Create(project);
      if (entity == null)
        return Result.InternalServerError("Failed to process project to entity");

      var response = await _repository.CreateAsync(entity);
      if (response != null)
      {
        var dto = ProjectFactory.Create(response); 
        return Result<ProjectDto>.Created(dto);
      }

      return Result.BadRequest("Error creating project");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("An unexpected error occurred while creating project");
    }
  }
  public async Task<IResult> GetAsync(string projectNumber)
  {
    if (string.IsNullOrWhiteSpace(projectNumber))
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
      if (project == null)
        return Result.InternalServerError("Failed to process project details to modell");

      return Result<ProjectDetails>.Ok(project);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching project with ID{projectNumber}: {ex}");
      return Result.InternalServerError($"{ex}");
    }
  }
  public async Task<IResult> UpdateAsync(ProjectDto dto)
  {
    if (dto != null)
    {
      var entity = ProjectFactory.Update(dto);

      var response = await _repository.UpdateAsync(entity);

      if (response)
        return Result.Ok();
    }

    return Result.BadRequest("Failed to update");
  }
  public async Task<IResult> GetAllAsync()
  {
    try
    {
      var respons = await _repository.GetAllAsync();

      if (respons == null || !respons.Any())
        return Result.NotFound("No projects found");

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