using Business.Dto;
using Business.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class ProjectDetails(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;
  protected override async Task OnInitializedAsync()
  {
    model = await _httpClient.GetFromJsonAsync<PressentationDetailsModel>($"api/projects/{ProjectNumber}");
  }

  [Parameter]
  public string ProjectNumber { get; set; } = null!;
  private PressentationDetailsModel? model { get; set; }
  private bool isEditing = false;

  public async Task SaveChangesAsync()
  {
    throw new NotImplementedException();
    //var response = await _httpClient.PutAsJsonAsync($"api/")
  }

}
