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

    [Inject]
    private IEnumerable<CustomerDto>? Customers { get; set; }
    private IEnumerable<EmployeeDto>? Employees { get; set; }
    private IEnumerable<ServiceTypeDto>? ServiceType { get; set; }
    private IEnumerable<StatusDto>? StatusType { get; set; }


    private ProjectDto? Project { get; set; } = new();
    private int SelectedServiceId { get; set; } = 0;
    private List<int> SelectedServiceIds { get; set; } = [];

    private string? message;
    protected override async Task OnInitializedAsync()
    {
      Customers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer") ?? [];
      Employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee") ?? [];
      ServiceType = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype") ?? [];
      StatusType = await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>("api/status") ?? [];
    }

    public async Task CreateProject()
    {
      // städa här
      var validationContext = new ValidationContext(Project);
      List<ValidationResult> validationResults = [];
      bool isValid = Validator.TryValidateObject(Project, validationContext, validationResults, true);
      if (!isValid)
      {
        message = "Project inhåller ogiltiga värden!";
        return;
      }
      if (SelectedServiceIds != null && SelectedServiceIds.Count > 0)
        Project.ServiceTypeIds = SelectedServiceIds;

      var response = await _httpClient.PostAsJsonAsync("api/projects/create-project", Project);

      if (response.IsSuccessStatusCode)
      {
        Project = new();
        message = "projekt skapat";
      }
      else
      {
        message = "något gick fel";
      }
    }
    public void AddService()
    {
      if (SelectedServiceId != 0 && !SelectedServiceIds!.Contains(SelectedServiceId))
      {
        SelectedServiceIds.Add(SelectedServiceId);
      }
    }
  }
}
