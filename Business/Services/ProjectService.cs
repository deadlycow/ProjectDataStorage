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
      return Result.BadRequest("Project cannot be null");

    var entity = ProjectFactory.Create(project);
    if (entity == null)
      return Result.InternalServerError("Failed to convert ProjectDto to entity");

    await _repository.BeginTransactionAsync();
    try
    {
      var response = await _repository.CreateAsync(entity);
      if (response == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("Failed to create project in database");
      }

      var affectedRows = await _repository.SaveAsync();
      if (affectedRows == 0)
      {
        await _repository.RollbackTransactionAsync();
        return Result.InternalServerError("No changes saved to the database");
      }

      await _repository.CommitTransactionAsync();
      return Result<ProjectDto>.Created(ProjectFactory.Create(response));

    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error creating project: {ex.Message}");
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
    if (dto == null)
      return Result.BadRequest("Project cannot be null");

    var entity = ProjectFactory.Update(dto);
    if (entity == null)
      return Result.BadRequest("Failed to convert ProjectDto to entity");

    await _repository.BeginTransactionAsync();
    try
    {
      _repository.Update(entity);
      var affectedRows = await _repository.SaveAsync();
      if (affectedRows == 0)
      {
        await _repository.RollbackTransactionAsync();
        return Result.InternalServerError("No changes saved to the database");
      }

      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch(Exception ex) 
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error updating project: {ex.Message}");
      return Result.InternalServerError("An errro occurred while updating the project");
    }
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
      return Result.BadRequest("Project number cannot be empty");

    await _repository.BeginTransactionAsync();
    try
    {
      var project = await _repository.GetAsync(x => x.ProjectNumber == projecNumber);
      if (project == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.NotFound($"Project with number {projecNumber} not found");
      }  

      _repository.Delete(project);
      var affectedRows = await _repository.SaveAsync();
      if (affectedRows == 0)
      {
        await _repository.RollbackTransactionAsync();
        return Result.InternalServerError("No changes saved to the database");
      }

      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting project {projecNumber}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting the project");
    }
  }
}