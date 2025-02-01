using Business.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pressentation_WebApp.Services;

namespace Pressentation_WebApp.Pages
{
  public partial class Projects : ComponentBase
  {
    [Inject]
    private ProjectsService _projectService { get; set; } = default!;
    private IEnumerable<PressentationModel>? ProjectsList { get; set; }

    protected override async Task OnInitializedAsync()
    {
      ProjectsList = await _projectService.GetProjects();
    }

    private async Task DeleteProject(string projectNumber)
    {
      var confirm = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this project?");
      if (confirm)
      {
        await _projectService.DeleteProject(projectNumber);
        ProjectsList = await _projectService.GetProjects();
      }
    }
  }
}
