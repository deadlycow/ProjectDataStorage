using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages
{
  public partial class Projects(NavigationManager navigationManager, HttpClient httpClient) : ComponentBase
  {
    private readonly NavigationManager navigationManager = navigationManager;
    private readonly HttpClient _httpClient = httpClient;


    [Inject]
    private IEnumerable<ProjectDto>? ProjectsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
      ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
    }

    private static void DeleteProject(string projectNumber)
    {
      //var confirm = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this project?");
      //if (confirm)
      //{
      //  await _projectService.DeleteProject(projectNumber);
      //  ProjectsList = await _projectService.GetProjects();
      //}
    }
    
    private void NavigateToProject(string projectNumber)
    {
      navigationManager.NavigateTo($"/project-details/{projectNumber}");
    }
  }
}
