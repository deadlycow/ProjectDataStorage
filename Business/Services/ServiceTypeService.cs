using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;
public class ServiceTypeService(IServiceRepository repository) : IServiceTypeService
{
  private readonly IServiceRepository _repository = repository;

  public async Task<IResult> CreateAsync(ServiceTypeDto dto)
  {
    if (dto == null)
      return Result.BadRequest("Service cannot be null");
    var entity = ServiceTypeFactory.Create(dto);
    if (entity == null)
      return Result.InternalServerError("Faild to convert ServiceTypeDto to entity");
    await _repository.BeginTransactionAsync();
    try
    {
      var response = await _repository.CreateAsync(entity);
      if (response == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("Faild to create service in database");
      }
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
      Debug.WriteLine($"Error creating service: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while creating service");
    }
  }

  public async Task<IResult> DeleteAsync(int id)
  {
    await _repository.BeginTransactionAsync();
    try
    {
      var service = await _repository.GetAsync(x => x.Id == id);
      if (service == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.NotFound($"Service with id {id} not found");
      }
      _repository.Delete(service);
      var affectedRows = await _repository.SaveAsync();
      if(affectedRows == 0)
      {  await _repository.RollbackTransactionAsync();
        return Result.BadRequest("No changes save to the database");
      }
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting service: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting the service");
    }
  }
  public Task<IResult> DeleteAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> GetAllAsync()
  {
    try
    {
      var response = await _repository.GetAllAsync();
      if (response == null || !response.Any())
        return Result.BadRequest("No services found");

      var services = ServiceTypeFactory.CreateList(response);
      if (services == null)
        return Result.BadRequest("Failed to process services to dto");

      return Result<IEnumerable<ServiceTypeDto>>.Ok(services);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("Error occurred while fetching services");
    }
  }
  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> UpdateAsync(ServiceTypeDto dto)
  {
    if (dto == null)
      return Result.BadRequest("Service cannot be null");
    var entity = ServiceTypeFactory.Update(dto);
    if (entity == null)
      return Result.BadRequest("Failed to convert ServiceTypeDto to entity");
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
    catch (Exception ex) {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error updating service: {ex.Message}");
      return Result.InternalServerError("An error occurred while updateing the service");
    }
  }
}
