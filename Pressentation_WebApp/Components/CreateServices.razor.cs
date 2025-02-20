﻿using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class CreateServices(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  [Parameter] public EventCallback OnServiceAdded { get; set; }
  [Parameter] public string ConfirmationMessage { get; set; } = null!;
  private ServiceTypeDto ServiceType { get; set; } = new();
  private string message = "Lägg till";
  private bool showConfirm = false;
  public async Task CreateService()
  {
    if (ServiceType == null)
      return;
    try
    {
      var postTask = await _httpClient.PostAsJsonAsync($"api/servicetype", ServiceType);
      postTask.EnsureSuccessStatusCode();

      await OnServiceAdded.InvokeAsync();
      showConfirm = true;
      ConfirmationMessage = $"Tjänst {ServiceType.Name} har lagts till.";
      ServiceType = new();
    }
    catch (HttpRequestException httpEx)
    {
      message = $"Network error: {httpEx.Message}";
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      message = $"An unexpected error occurred: {ex.Message}";
    }
  }
  public void CloseConfirm()
  {
    showConfirm = false;
  }
}
