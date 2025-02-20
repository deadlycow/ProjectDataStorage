using Business.Dto;
using Microsoft.AspNetCore.Components;

namespace Pressentation_WebApp.Components;
public partial class EditCustomer
{
  [Parameter] public CustomerDto Customer { get; set; } = new();
  [Parameter] public EventCallback<CustomerDto> OnConfirmed { get; set; }
  [Parameter] public EventCallback OnClose { get; set; }


  private async Task Confirm() => await OnConfirmed.InvokeAsync(Customer);
  private async Task Close() => await OnClose.InvokeAsync();
}
