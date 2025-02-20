using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class CreateServices(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  [Parameter] public EventCallback OnServiceAdded { get; set; }

  private ServiceTypeDto ServiceType { get; set; } = new();
  private string message = "Lägg till";
  public async Task CreateService()
  {
    if (ServiceType == null)
      return;
    try
    {
      var postTask = await _httpClient.PostAsJsonAsync($"api/servicetype", ServiceType);
      postTask.EnsureSuccessStatusCode();

      await OnServiceAdded.InvokeAsync();
      ServiceType = new();
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
