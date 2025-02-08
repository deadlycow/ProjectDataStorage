using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController(IStatusTypeService statusTypeService) : ControllerBase
{
  private readonly IStatusTypeService _statusTypeService = statusTypeService;
  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var response = await _statusTypeService.GetAllAsync();
    if (response is Result<IEnumerable<StatusDto>> statusTypes)
      return Ok(statusTypes.Data);

    return BadRequest(response);
  }
}
