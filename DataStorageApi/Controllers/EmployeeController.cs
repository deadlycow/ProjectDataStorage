using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
  private readonly IEmployeeService _employeeService = employeeService;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var response = await _employeeService.GetAllAsync();

    if (response is Result<IEnumerable<EmployeeDto>> emplyees)
      return Ok(emplyees.Data);

    return BadRequest(response);
  }
  [HttpPost]
  public async Task<IActionResult> Create(EmployeeDto dto)
  {
    if (dto == null)
      return BadRequest("Employee data is required");
    try
    {
      var response = await _employeeService.CreateAsync(dto);
      if (response.Success)
        return Ok();

      return BadRequest(response.ErrorMessage);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteCustomer(int id)
  {
    if (id < 1)
      return BadRequest("Invalid number range");
    try
    {
      var result = await _employeeService.DeleteAsync(id);
      return result.StatusCode switch
      {
        200 => Ok(),
        400 => BadRequest(result.ErrorMessage),
        404 => NotFound(result.ErrorMessage),
        _ => StatusCode(500, result.ErrorMessage)
      };
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  [HttpPut]
  public async Task<IActionResult> UpdateCustomer([FromBody] EmployeeDto dto)
  {
    if (dto == null)
      return BadRequest("Employee data is null");
    try
    {
      var response = await _employeeService.UpdateAsync(dto);
      if (response.Success)
        return Ok(response);

      return BadRequest("Employee was NOT updated");
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}
