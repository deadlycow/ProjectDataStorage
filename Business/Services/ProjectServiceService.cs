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
    try
    {
      var existingServices = await _repository.GetAllAsync(q => q.Where(s => s.ProjectId == id));

      if (existingServices != null && existingServices.Any())
      {
        await _repository.RemoveListAsync(existingServices);
        var response = await _repository.CreateListAsync(ProjectServiceFactory.Create(listDto));
        if (response)
          return Result.Ok();
      }
      else
      {
        var result = await _repository.CreateListAsync(ProjectServiceFactory.Create(id, listDto));
        if (result)
          return Result.Ok();
      }

      return Result.AlreadyExists("No changes made");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return Result.InternalServerError("Something broke");
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
    try
    {
      var existingServices = await _repository.GetAllAsync(q => q.Where(s => s.ProjectId == id));

      if (existingServices != null)
      {
        var response = await _repository.RemoveListAsync(existingServices);
        if (response)
          return Result.Ok();
      }
      return Result.AlreadyExists("No changes made");
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return Result.InternalServerError("Something broke");
    }
  }

  public Task<IResult> UpdateAsync(ProjectServiceDto entity)
  {
    throw new NotImplementedException();
  }
}
