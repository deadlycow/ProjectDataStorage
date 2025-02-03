using Business.Dto;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataStorageApi.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
  private readonly IProjectService _projectService = projectService;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _projectService.GetAllAsync();
    
    if (result is Result<IEnumerable<PressentationModel>> projects)
      return Ok(projects.Data);

    return BadRequest();
  }

  [HttpGet("{projectNumber}")]
  public async Task<IActionResult> GetAsync(string projectNumber)
  {
    var result = await _projectService.GetByExpressionAsync(projectNumber);
    if (result is Result<PressentationDetailsModel> projectDetails)
      return Ok(projectDetails.Data);

    return NotFound();
  }

  //[HttpPut]
  //public async Task<IActionResult> UpdateProject([FromBody] PressentationDetailsModel updatedProject)
  //{
  //  var result = await _projectService.UpdateAsync()
  //}
}
