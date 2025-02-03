using Business.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class ProjectDetails(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  //[Parameter]
  //public string projectNumber { get; set; } = null!;
  //private PressentationDetailsModel? DetailsModel { get; set; }

  //protected override async Task OnInitializedAsync()
  //{
  //  DetailsModel = await _httpClient.GetFromJsonAsync<PressentationDetailsModel>($"api/projects/{projectNumber}");
  //}
}
