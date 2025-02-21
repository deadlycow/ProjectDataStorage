using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;
public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
  private readonly IEmployeeRepository _repository = repository;

  public async Task<IResult> CreateAsync(EmployeeDto dto)
  {
    if (dto == null)
      return Result.BadRequest("Employee cannot be null");
    var entity = EmployeeFactory.Create(dto);
    if (entity == null)
      return Result.InternalServerError("Failed to convert EmployeeDto to entity");
    await _repository.BeginTransactionAsync();
    try
    {
      var response = await _repository.CreateAsync(entity);
      if (response == null)
      {
        await _repository.RollbackTransactionAsync();
        return Result.BadRequest("Failed to create employee in database");
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
      return Result.InternalServerError("An unexpected error occurred while creating employee");
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
        return Result.NotFound($"Employee with id {id} not found");
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
      Debug.WriteLine($"Error deleting employee: {id}: {ex.Message}");
      return Result.InternalServerError("An error occurred while deleting employee");
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
        return Result.BadRequest("No employees");

      var employees = EmployeeFactory.CreateList(response);

      return Result<IEnumerable<EmployeeDto>>.Ok(employees);
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex);
      return Result.InternalServerError("Error occurred while fetching employees");
    }
  }

  public Task<IResult> GetAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> UpdateAsync(EmployeeDto dto)
  {
    if (dto == null)
      return Result.BadRequest("Employee cannot be null");

    var entity = EmployeeFactory.Update(dto);
    if (entity == null)
      return Result.BadRequest("Failed to convert EmployeeDto to entity");

    await _repository.BeginTransactionAsync();
    try
    {
      _repository.Update(entity);
      var affecteRows = await _repository.SaveAsync();
      if (affecteRows == 0)
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
      Debug.WriteLine($"Error updating employee: {ex.Message}");
      return Result.InternalServerError("An error occurred while updating the employee");
    }
  }
}
