using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class CreateCustomer(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;
  [Parameter] public EventCallback OnCustomerAdded { get; set; }
  [Parameter] public string ConfirmationMessage { get; set; } = null!;
  [Parameter] public bool Show { get; set; } = false;

  private CustomerDto Customer { get; set; } = new();
  private string message = "Lägg till";
  private bool showConfirm = false;
  public async Task CreateCustomers()
  {
    if (Customer == null)
      return;
    try
    {
      var postTask = await _httpClient.PostAsJsonAsync("api/customer", Customer);
      postTask.EnsureSuccessStatusCode();

      await OnCustomerAdded.InvokeAsync();
      ConfirmationMessage = $"Kunden {Customer.Name} har skapats.";
      Customer = new();
      showConfirm = true;
    }
    catch(HttpRequestException httpEx)
    {
      message = $"Network error: {httpEx.Message}";
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      message = "An unexpected error occurred";
    }
  }
  public void CloseConfirm()
  {
    showConfirm = false;
  }
}
