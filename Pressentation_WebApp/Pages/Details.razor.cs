using Business.Dto;
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class Details(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;
  protected override async Task OnInitializedAsync()
  {
    Project = await _httpClient.GetFromJsonAsync<ProjectDetails>($"api/projects/{ProjectNumber}");
    Employees = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>($"api/employee");
    Customers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>($"api/customer");
    Status = await _httpClient.GetFromJsonAsync<IEnumerable<StatusDto>>($"api/status");
    MapServicesToList();
    Id = Project!.Id;
  }

  [Parameter]
  public string ProjectNumber { get; set; } = null!;
  private ProjectDetails? Project { get; set; }
  private List<ProjectServiceDto> ServiceList { get; set; } = [];
  private IEnumerable<EmployeeDto>? Employees { get; set; }
  private IEnumerable<CustomerDto>? Customers { get; set; }
  private IEnumerable<StatusDto>? Status { get; set; }
  private bool isEditing = false;
  [Parameter]
  public int Id { get; set; } 
  private string buttonStatus = "Spara ändringar";

  public async Task SaveChangesAsync()
  {
    if (Project == null)
      return;

    try
    {
      var putTask = _httpClient.PutAsJsonAsync($"api/projects", Project);
      var postTask = _httpClient.PostAsJsonAsync($"api/projectservice/{Project.Id}", ServiceList);

      var responses = await Task.WhenAll(putTask, postTask);

      if (responses.All(r => r.IsSuccessStatusCode))
      {
        buttonStatus = "Projekt uppdaterat";
        isEditing = false;
        return;
      }
      buttonStatus = $"Ett fel inträffa";
    }
    catch (Exception ex){
      Debug.WriteLine(ex.Message);
      buttonStatus = "Error";
    }
  }
  public void RemoveService(int serviceId)
  {
    var item = ServiceList.FirstOrDefault(s => s.ServiceId == serviceId);
    if (item != null)
      ServiceList.Remove(item);
  }
  public void MapServicesToList()
  {
    if (Project != null)
    {
      foreach (var service in Project.Services)
      {
        ServiceList.Add(new ProjectServiceDto { ProjectId = Project.Id, ServiceId = service.Id });
      }
    }
  }
}
