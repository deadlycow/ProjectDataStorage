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

    private async Task DeleteProject(string projectNumber)
    {
      var response = await _httpClient.DeleteAsync($"api/projects/{projectNumber}");
      if (response.IsSuccessStatusCode)
        ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
    }

    private void NavigateToProject(string projectNumber)
    {
      navigationManager.NavigateTo($"/details/{projectNumber}");
    }
  }
}
