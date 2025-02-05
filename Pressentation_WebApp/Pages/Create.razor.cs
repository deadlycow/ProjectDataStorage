using Business.Dto;
using Microsoft.AspNetCore.Components;
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
    protected override async Task OnInitializedAsync()
    {
      Customers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer") ?? [];
      Employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee") ?? [];
      ServiceType = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype") ?? [];
    }
    private bool isEditing { get; set; } = false;

    private CustomerDto model = new();
    public static void CreateProject()
    {
    }
  }
}
