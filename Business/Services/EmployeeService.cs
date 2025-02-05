using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;
public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;

  public Task<IResult> CreateAsync(EmployeeDto entity)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> DeleteAsync(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<IResult> GetAllAsync()
  {
    var response = await _employeeRepository.GetAllAsync();
    if (response == null)
      return Result.BadRequest("No employees");
    var employees = EmployeeFactory.CreateList(response);
    return Result<IEnumerable<EmployeeDto>>.Ok(employees);
  }

  public Task<IResult> GetByExpressionAsync(string id)
  {
    throw new NotImplementedException();
  }

  public Task<IResult> UpdateAsync(string id, EmployeeDto entity)
  {
    throw new NotImplementedException();
  }
}
