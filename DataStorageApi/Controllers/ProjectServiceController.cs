using Business.Dto;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectServiceController(IProjectServiceService repository) : ControllerBase
  {
    private readonly IProjectServiceService _repository = repository;

    [HttpPost("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] List<ProjectServiceDto> serviceList)
    {
      if (serviceList.Count > 0)
      {
        var respons = await _repository.CreateList(id, serviceList);
        if (respons.Success)
          return Ok();
      }
      else
      {
        var response = await _repository.Remove(id, serviceList);
        if (response.Success)
          return Ok();
      }
      return BadRequest();
    }
  }
}
