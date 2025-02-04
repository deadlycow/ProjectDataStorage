using Business.Dto;
using Business.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages
{
  public partial class Create(HttpClient httpClient)
  {
    private readonly HttpClient _httpClient = httpClient;

    [Inject]
    private IEnumerable<CustomerDto>? Customers { get; set; }
    protected override async Task OnInitializedAsync()
    {
      Customers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer");
    }
    private bool isEditing { get; set; } = false;

    private PressentationDetailsModel model = new();
    public static void CreateProject()
    {
    }
  }
}
