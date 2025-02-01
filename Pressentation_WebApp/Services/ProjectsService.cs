using Business.Models;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Services
{
  public class ProjectsService(HttpClient httpClient)
  {
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<PressentationModel>?> GetProjects()
    {
      return await _httpClient.GetFromJsonAsync<IEnumerable<PressentationModel>>("api/projects");
    }
  }
}
