using Business.Dto;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageApi.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
  private readonly ICustomerService _customerService = customerService;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    try
    {
      var response = await _customerService.GetAllAsync();

      if (response is Result<IEnumerable<CustomerDto>> customers)
      {
        if (customers.Data == null || !customers.Data.Any())
          return NoContent();
        return Ok(customers.Data);
      }

      return BadRequest(response);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  [HttpPost]
  public async Task<IActionResult> Create(CustomerDto dto)
  {
    if (dto == null)
      return BadRequest("Customer data is required");
    try
    {
      var response = await _customerService.CreateAsync(dto);
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
      var result = await _customerService.DeleteAsync(id);
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
  public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto dto)
  {
    if (dto == null)
      return BadRequest("Project data is null");
    try
    {
      var response = await _customerService.UpdateAsync(dto);
      if (response.Success)
        return Ok(response);

      return BadRequest("Customer was NOT updated");
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}