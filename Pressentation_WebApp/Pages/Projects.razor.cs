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
    private bool showConfirmDialog = false;
    private string projectToDelete = "";

    protected override async Task OnInitializedAsync()
    {
      ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
    }

    private async Task DeleteProject(bool confirm)
    {
      showConfirmDialog = false;
      if (confirm)
      {
        var response = await _httpClient.DeleteAsync($"api/projects/{projectToDelete}");

        if (response.IsSuccessStatusCode)
          ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
      }
    }

    private void NavigateToProject(string projectNumber)
    {
      navigationManager.NavigateTo($"/details/{projectNumber}");
    }

    private void ConfirmDelete(string projectNumber)
    {
      projectToDelete = projectNumber;
      showConfirmDialog = true;
    }
  }
}
