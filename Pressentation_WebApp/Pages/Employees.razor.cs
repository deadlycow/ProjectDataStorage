using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class Employees(HttpClient httpClient) : ComponentBase
{
  private readonly HttpClient _httpClient = httpClient;

  protected override async Task OnInitializedAsync()
  {
    EmployeesList = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee");
  }

  private IEnumerable<EmployeeDto>? EmployeesList { get; set; }
  private EmployeeDto? Employee { get; set; } = new();
  private bool showConfirmDialog = false;
  private bool showEmployee = false;
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
  private async Task UpdateEmployee(EmployeeDto employee)
  {
    if (employee == null)
      return;
    try
    {
      var response = await _httpClient.PutAsJsonAsync("api/employee", employee);
      response.EnsureSuccessStatusCode();
      await LoadEmployees();
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"An error occurred while updating the employee: {ex.Message}");
    }
    showEmployee = false;
  }
  private async Task LoadEmployees()
  {
    EmployeesList = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employee");
  }
  private void ConfirmDelete(int employeeId)
  {
    employeeToDelete = employeeId;
    showConfirmDialog = true;
  }
  private void OpenEditDialog(EmployeeDto employee)
  {
    Employee = employee;
    showEmployee = true;
  }
  private void CloseEditDialog()
  {
    showEmployee = false;
    Employee = null;
  }
}
