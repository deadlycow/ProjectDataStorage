using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataStorageApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ServiceTypeController(IServiceTypeService serviceTypeService) : ControllerBase
  {
    private readonly IServiceTypeService _serviceTypeService = serviceTypeService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var response = await _serviceTypeService.GetAllAsync();
        if (response is Result<IEnumerable<ServiceTypeDto>> services)
          return Ok(services.Data);

        return BadRequest(response);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"An error occurred while retrieving service types. {ex.Message}");
        return StatusCode(500, "An internal server error occurred");
      }
    }
    [HttpPost]
    public async Task<IActionResult> Create(ServiceTypeDto dto)
    {
      if (dto == null)
        return BadRequest("Service data is required");
      try
      {
        var response = await _serviceTypeService.CreateAsync(dto);
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
    public async Task<IActionResult> DeleteService(int id)
    {
      if (id < 1)
        return BadRequest("Invalid number range");
      try
      {
        var result = await _serviceTypeService.DeleteAsync(id);
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
    public async Task<IActionResult> UpdateService(ServiceTypeDto dto)
    {
      if (dto == null)
        return BadRequest("UpdateService data is required");
      try
      {
        var result = await _serviceTypeService.UpdateAsync(dto);
        if (result.Success)
          return Ok();

        return BadRequest(result.ErrorMessage);
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}
