using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Data.Repositories;
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
    var response = await _customerService.GetAllAsync();
    
    if (response is Result<IEnumerable<CustomerDto>> customers)
      return Ok(customers.Data);

    return BadRequest(response);
  }
}
