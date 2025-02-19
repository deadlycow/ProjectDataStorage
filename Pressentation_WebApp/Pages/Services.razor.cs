using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;

public partial class Services(NavigationManager navigationManager, HttpClient httpClient) : ComponentBase
{
  private readonly NavigationManager _navigationManager = navigationManager;
  private readonly HttpClient _httpClient = httpClient;

  protected override async Task OnInitializedAsync()
  {
    ServiceList = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype");
  }

  private IEnumerable<ServiceTypeDto>? ServiceList { get; set; }
  private bool showConfirmDialog = false;
  private int serviceToDelete;
  private ServiceTypeDto? ServiceType { get; set; } = new();
  private async Task DeleteService(bool confirm)
  {
    showConfirmDialog = false;

    if (!confirm)
      return;

    try
    {
      var response = await _httpClient.DeleteAsync($"api/servicetype/{serviceToDelete}");
      response.EnsureSuccessStatusCode();

      ServiceList = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype");
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
    serviceToDelete = employeeId;
    showConfirmDialog = true;
  }
  private async Task LoadServices()
  {
    ServiceList = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype");
    StateHasChanged();
  }
}