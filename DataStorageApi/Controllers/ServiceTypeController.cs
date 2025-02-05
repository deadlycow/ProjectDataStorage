using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
      var response = await _serviceTypeService.GetAllAsync();
      if (response is Result<IEnumerable<ServiceTypeDto>> services) 
        return Ok(services.Data);

      return BadRequest(response);
    }
  }
}
