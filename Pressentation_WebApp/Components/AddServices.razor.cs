using Business.Dto;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Components;
public partial class AddServices(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  protected override async Task OnInitializedAsync()
  {
    Services = await _httpClient.GetFromJsonAsync<IEnumerable<ServiceTypeDto>>("api/servicetype") ?? [];
  }

  [Parameter]
  public IEnumerable<ServiceTypeDto>? Services { get; set; }
  [Parameter]
  public bool IsEditing { get; set; } = false;
  [Parameter]
  public List<ProjectServiceDto> ProjectServices { get; set; } = [];
  [Parameter]
  public int Id { get; set; }
  private int _value;

  private void AddToList()
  {
    if (_value > 0 && !ProjectServices.Any(s => s.ServiceId == _value))
      ProjectServices.Add( new ProjectServiceDto
      {
        ProjectId = Id,
        ServiceId = _value,
      });
  }
  private void RemoveFromList(ProjectServiceDto dto)
  {
    ProjectServices.Remove(dto);
  }
}
