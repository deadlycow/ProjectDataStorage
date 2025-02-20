using Business.Dto;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Pressentation_WebApp.Pages;
public partial class Customers(HttpClient httpClient)
{
  private readonly HttpClient _httpClient = httpClient;

  protected override async Task OnInitializedAsync()
  {
    CustomersList = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer");
  }

  private IEnumerable<CustomerDto>? CustomersList { get; set; }
  private CustomerDto? Customer { get; set; } = new();
  private bool showConfirmDialog = false;
  private bool showCustomer = false;
  private int customerToDelete;


  public async Task DeleteCustomer(bool confirm)
  {
    showConfirmDialog = false;
    if (!confirm)
      return;
    try
    {
      var response = await _httpClient.DeleteAsync($"api/customer/{customerToDelete}");
      response.EnsureSuccessStatusCode();

      CustomersList = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer");
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
  public void ConfirmDelete(int customerId)
  {
    customerToDelete = customerId;
    showConfirmDialog = true;
  }
  private async Task LoadCustomer()
  {
    CustomersList = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customer");
    StateHasChanged();
  }
  private void OpenEditDialog(CustomerDto customer)
  {
    Customer = customer;
    showCustomer = true;
  }
  private void CloseEditDialog()
  {
    showCustomer = false;
    Customer = null;
  }
}
