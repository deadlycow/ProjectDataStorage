using Business.Dto;
using Microsoft.AspNetCore.Components;

namespace Pressentation_WebApp.Components;
public partial class EditEmployee
{
  [Parameter] public EmployeeDto Employee { get; set; } = new();
  [Parameter] public EventCallback<EmployeeDto> OnConfirmed { get; set; }
  [Parameter] public EventCallback OnClose { get; set; }


  private async Task Confirm() => await OnConfirmed.InvokeAsync(Employee);
  private async Task Close() => await OnClose.InvokeAsync();
}
