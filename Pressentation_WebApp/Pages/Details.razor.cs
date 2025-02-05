using Business.Dto;
using Business.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class Details(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;
  protected override async Task OnInitializedAsync()
  {
    Project = await _httpClient.GetFromJsonAsync<ProjectDetails>($"api/projects/{ProjectNumber}");
    //Status = await _httpClient.GetFromJsonAsync<StatusDto>($"api/stat")
  }

  [Parameter]
  public string ProjectNumber { get; set; } = null!;
  private ProjectDetails? Project { get; set; }
  private StatusDto? Status { get; set; }
  private bool isEditing = false;

  public Task SaveChangesAsync()
  {
    throw new NotImplementedException();
    //var response = await _httpClient.PutAsJsonAsync($"api/")
  }

}
