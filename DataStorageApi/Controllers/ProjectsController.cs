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
    var response = await _projectService.GetAllAsync();
    
    if (response is Result<IEnumerable<ProjectDto>> projects)
      return Ok(projects.Data);

    return BadRequest();
  }

  [HttpGet("{projectNumber}")]
  public async Task<IActionResult> GetAsync(string projectNumber)
  {
    var response = await _projectService.GetAsync(projectNumber);
    if (response is Result<ProjectDetails> projectDetails)
      return Ok(projectDetails.Data);

    return NotFound();
  }

  [HttpDelete("{projectNumber}")]
  public async Task<IActionResult> DeleteProject(string projectNumber)
  {
    var result = await _projectService.DeleteAsync(projectNumber);

    return result.StatusCode switch
    {
      200 => Ok(),
      404 => NotFound(result.ErrorMessage),
      400 => BadRequest(result.ErrorMessage),
      _ => StatusCode(500, result.ErrorMessage)
    };
  }

  [HttpPost("create-project")]
  public async Task<IActionResult> CreateProject([FromBody]ProjectDto dto)
  {
    var response = await _projectService.CreateAsync(dto);
    if (response.Success)
      return Ok(response);

    return BadRequest();
  }
}
