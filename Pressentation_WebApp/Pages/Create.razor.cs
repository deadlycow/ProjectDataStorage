using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages
{
  public partial class Create(HttpClient httpClient)
  {
    private readonly HttpClient _httpClient = httpClient;
    protected override async Task OnInitializedAsync()
    {
      Customers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer") ?? [];
      Employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee") ?? [];
      StatusType = await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("api/status") ?? [];
    }
    [Parameter]
    public List<ProjectServiceDto> Services { get; set; } = [];

    private IEnumerable<CustomerDto>? Customers { get; set; }
    private IEnumerable<EmployeeDto>? Employees { get; set; }
    private IEnumerable<StatusDto>? StatusType { get; set; }

    private ProjectDto? Project { get; set; } = new();
    private string? message;
    private readonly List<ValidationResult> validationResults = [];

    public async Task CreateProject()
    {
      if (Project == null)
        return;
      try
      {
        var validationContext = new ValidationContext(Project);
        Validator.TryValidateObject(Project, validationContext, validationResults, true);

        var postTask = await _httpClient.PostAsJsonAsync("api/projects/create-project", Project);
        postTask.EnsureSuccessStatusCode();

        var createdProject = await postTask.Content.ReadFromJsonAsync<ProjectDto>();
        postTask = await _httpClient.PostAsJsonAsync($"api/projectservice/{createdProject!.Id}", Services);
        postTask.EnsureSuccessStatusCode();

        Project = new();
        Services = [];
        message = "projekt skapat";
      }
      catch (HttpRequestException httpEx)
      {
        message = $"Nätverksfel: {httpEx.Message}";
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex.Message);
        message = $"Ett oväntat fel inträffade";
      }

    }
  }
}
