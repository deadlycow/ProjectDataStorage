using Business.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
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
      ServiceType = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype") ?? [];
      StatusType = await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("api/status") ?? [];
    }
    [Parameter]
    public List<ProjectServiceDto> Services { get; set; } = [];

    private IEnumerable<CustomerDto>? Customers { get; set; }
    private IEnumerable<EmployeeDto>? Employees { get; set; }
    private IEnumerable<ServiceTypeDto>? ServiceType { get; set; }
    private IEnumerable<StatusDto>? StatusType { get; set; }

    private ProjectDto? Project { get; set; } = new();
    private string? message;
    //private int SelectedServiceId { get; set; } = 0;
    //private List<int> SelectedServiceIds { get; set; } = [];

    public async Task CreateProject()
    {
      if (Project == null)
        return;

      var validationContext = new ValidationContext(Project);
      List<ValidationResult> validationResults = [];
      //bool isValid = 
      Validator.TryValidateObject(Project, validationContext, validationResults, true);
      //if (!isValid)
      //{
      //  message = "Project inhåller ogiltiga värden!";
      //  return;
      //}

      var postTask = await _httpClient.PostAsJsonAsync("api/projects/create-project", Project);
      if (!postTask.IsSuccessStatusCode)
      {
        message = "Projektet kunde inte skapas korrekt";
        return;
      }

      var createdProject = await postTask.Content.ReadFromJsonAsync<ProjectDto>();
      var postTasdk = await _httpClient.PostAsJsonAsync($"api/projectservice/{createdProject.Id}", Services);
      if (!postTasdk.IsSuccessStatusCode)
      {
        message = "Något gick fel vid skapandet av tjänster";
        return;
      }

      Project = new();
      Services = [];
      message = "projekt skapat";

    }
  }
}
