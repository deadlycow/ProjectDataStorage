using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages
{
  public partial class Projects(NavigationManager navigationManager, HttpClient httpClient) : ComponentBase
  {
    private readonly NavigationManager navigationManager = navigationManager;
    private readonly HttpClient _httpClient = httpClient;
    
    protected override async Task OnInitializedAsync()
    {
      ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
      Status = await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("api/status");
    }
    private IEnumerable<ProjectDto>? ProjectsList { get; set; }
    public IEnumerable<StatusDto>? Status { get; set; }
    
    private bool showConfirmDialog = false;
    private string projectToDelete = "";

    private async Task DeleteProject(bool confirm)
    {
      showConfirmDialog = false;

      if (!confirm)
        return;

      try
      {
        var response = await _httpClient.DeleteAsync($"api/projects/{projectToDelete}");
        response.EnsureSuccessStatusCode();

        ProjectsList = await _httpClient.GetFromJsonAsync<IEnumerable<ProjectDto>>("api/projects");
      }
      catch (HttpRequestException httpEx)
      {
        Debug.WriteLine($"Nätverksfel: {httpEx.Message}");
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"Ett oväntat fel inträffade: {ex.Message}");
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
