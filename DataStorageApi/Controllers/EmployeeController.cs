using Business.Dto;
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
}
