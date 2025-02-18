using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages; 
public partial class Employees(NavigationManager navigationManager, HttpClient httpClient) : ComponentBase
{
  private readonly NavigationManager _navigationManager = navigationManager;
  private readonly HttpClient _httpClient = httpClient;
  
  protected override async Task OnInitializedAsync()
  {
    EmployeesList = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee");
  }

  private IEnumerable<EmployeeDto>? EmployeesList { get; set; }
  private bool showConfirmDialog = false;
  private int employeeToDelete;
  private async Task DeleteProject(bool confirm)
  {
    showConfirmDialog = false;

    if (!confirm)
      return;

    try
    {
      var response = await _httpClient.DeleteAsync($"api/employee/{employeeToDelete}");
      response.EnsureSuccessStatusCode();

      EmployeesList = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee");
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
  private void ConfirmDelete(int employeeId)
  {
    employeeToDelete = employeeId;
    showConfirmDialog = true;
  }
}
