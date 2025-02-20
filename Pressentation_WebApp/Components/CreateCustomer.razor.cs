using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class CreateCustomer(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;
  [Parameter] public EventCallback OnCustomerAdded { get; set; }

  private CustomerDto Customer { get; set; } = new();
  private string message = "Lägg till";
  public async Task CreateCustomers()
  {
    if (Customer == null)
      return;
    try
    {
      var postTask = await _httpClient.PostAsJsonAsync("api/customer", Customer);
      postTask.EnsureSuccessStatusCode();

      await OnCustomerAdded.InvokeAsync();
      Customer = new();
    }
    catch(HttpRequestException httpEx)
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
