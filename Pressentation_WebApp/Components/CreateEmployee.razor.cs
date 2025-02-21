using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class CreateEmployee(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  [Parameter] public EventCallback OnEmployeeAdded { get; set; }
  [Parameter] public string ConfirmationMessage { get; set; } = null!;
  [Parameter] public bool Show {  get; set; } = false;

  private EmployeeDto Employee { get; set; } = new();
  private string message = "Lägg till";
  private bool showConfirm = false;

  public async Task CreateEmployees()
  {
    if (Employee == null)
      return;
    try
    {
      var postTask = await _httpClient.PostAsJsonAsync("api/employee", Employee);
      postTask.EnsureSuccessStatusCode();

      await OnEmployeeAdded.InvokeAsync();
      ConfirmationMessage = $"Anställd {Employee.Name} har skapats";
      Employee = new();
      showConfirm = true;
    }
    catch (HttpRequestException httpEx)
    {
      message = $"Network error: {httpEx.Message}";
    }
    catch (Exception ex) { 
      Debug.WriteLine(ex.Message);
      message = "An unexpected error occurred";
    }
  }
  public void CloseConfirm()
  {
    showConfirm = false;
  }
}
