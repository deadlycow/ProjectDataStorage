using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;
public class CustomerService(ICustomerRepository repository) : ICustomerService
{
  private readonly ICustomerRepository _repository = repository;

  public async Task<IResult> CreateAsync(CustomerDto dto)
  {
    if (dto == null)
      return Result.BadRequest("Customer cannot be null");
    var entity = CustomerFactory.Create(dto);
    if (entity == null)
      return Result.InternalServerError("Failed to convert CustomerDto to entity");
    await _repository.BeginTransactionAsync();
    try
    {
      var response = await _repository.CreateAsync(entity);
      if (response == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("Failed to create customer in database");
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
      Debug.WriteLine($"Error creating customer: {ex.Message}");
      return Result.InternalServerError("An unexpected error occurred while creating customer");
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
        return Result.NotFound($"Customer with id {id} not found");
      }
      _repository.Delete(service);
      var affectedRows = await _repository.SaveAsync();
      if (affectedRows == 0)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("No changes save to the database");
      }
      await _repository.CommitTransactionAsync();
      return Result.Ok();
    }
    catch (Exception ex)
    {
      await _repository.RollbackTransactionAsync();
      Debug.WriteLine($"Error deleting customer: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting customer");
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
      var respons = await _repository.GetAllAsync();
      if (respons == null || !respons.Any())
        return Result.BadRequest("No customers");

      var customers = CustomerFactory.CreateList(respons);

      return Result<IEnumerable<CustomerDto>>.Ok(customers);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("Error occurred while fetching customers");
    }
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(CustomerDto entity)
  {
    throw new NotImplementedException();
  }
}
