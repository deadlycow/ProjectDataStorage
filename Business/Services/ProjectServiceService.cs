using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;
public class ProjectServiceService(IProjectServiceRepository repository) : IProjectServiceService
{
  private readonly IProjectServiceRepository _repository = repository;

  public Task<IResult> CreateAsync(ProjectServiceDto entity)
  {
    throw new NotImplementedException();
  }
  public async Task<IResult> CreateList(int id, List<ProjectServiceDto> listDto)
  {
    if (id < 1)
      return Result.BadRequest("Id is 0");

    await _repository.BeginTransactionAsync();
    try
    {
      var existingServices = await _repository.GetAllAsync(q => q.Where(s => s.ProjectId == id));

      if (existingServices?.Any() == true)
      {
        _repository.RemoveList(existingServices);
      }

      var newServices = ProjectServiceFactory.Create(id, listDto);
      _repository.CreateListAsync(newServices);

      var affectedRows = await _repository.SaveAsync();
      if (affectedRows == 0)
      {
        await _repository.RollbackTransactionAsync();
        return Result.InternalServerError("No changes were saved to the database");
      }

      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error creating service list for Project ID {id}: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred");
    }
  }

  public Task<IResult> DeleteAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> Remove(int id, List<ProjectServiceDto> listDto)
  {
    if (id < 1)
      return Result.BadRequest("Invalid project ID");

    await _repository.BeginTransactionAsync();
    try
    {
      var existingServices = await _repository.GetAllAsync(q => q.Where(s => s.ProjectId == id));

      if (existingServices == null || !existingServices.Any())
      {
        await _repository.RollbackTransactionAsync();
        return Result.NotFound($"No services found for project ID {id}");
      }

      _repository.RemoveList(existingServices);
      var affectedRows = await _repository.SaveAsync();

      if (affectedRows == 0)
      {
        await _repository.CommitTransactionAsync();
        return Result.InternalServerError("No changes saved to the database");
      }
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine(ex.Message);
      return Result.InternalServerError("An unexpected error occurred");
    }
  }

  public Task<IResult> UpdateAsync(ProjectServiceDto entity)
  {
    throw new NotImplementedException();
  }
}
