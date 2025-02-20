using Business.Dto;
using Business.Factories;
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
    try
    {
      var response = await _projectService.GetAllAsync();

      if (response is Result<IEnumerable<ProjectDto>> projects)
      {
        if (projects.Success)
          return Ok(projects.Data);

        return BadRequest(projects.ErrorMessage);
      }
      return BadRequest("Unexpected response format");
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpGet("{projectNumber}")]
  public async Task<IActionResult> GetAsync(string projectNumber)
  {
    if (string.IsNullOrWhiteSpace(projectNumber))
      return BadRequest("Project number cannot be empty or whitespace");
    try
    {
      var response = await _projectService.GetAsync(projectNumber);

      if (response is Result<ProjectDetails> projectDetails)
      {
        if (projectDetails.Success)
          return Ok(projectDetails.Data);

        return BadRequest(projectDetails.ErrorMessage);
      }
      return NotFound("Project not found");
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpDelete("{projectNumber}")]
  public async Task<IActionResult> DeleteProject(string projectNumber)
  {
    if (string.IsNullOrWhiteSpace(projectNumber))
      return BadRequest("Project number cannot be empty or whitespace");
    try
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
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpPost("create-project")]
  public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] ProjectDto dto)
  {
    if (dto == null)
      return BadRequest("Project data is required");
    try
    {
      var response = await _projectService.CreateAsync(dto);
      if (response.Success && response is Result<ProjectDto> item)
        return Ok(item.Data);


      return BadRequest(response.ErrorMessage);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
  [HttpPut]
  public async Task<IActionResult> UpdateProject([FromBody] ProjectDetails form)
  {
    if (form == null)
      return BadRequest("Project data is null");
    try
    {
      var dto = ProjectFactory.Update(form);
      var response = await _projectService.UpdateAsync(dto);
      if (response.Success)
        return Ok(response);

      return BadRequest("Project was NOT updated");
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}
