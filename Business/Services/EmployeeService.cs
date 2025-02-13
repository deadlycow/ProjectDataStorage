using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

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
    try
    {
      var response = await _employeeRepository.GetAllAsync();
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

  public Task<IResult> UpdateAsync(EmployeeDto entity)
  {
    throw new NotImplementedException();
  }
}
