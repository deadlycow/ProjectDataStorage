﻿using Business.Dto;
using Microsoft.AspNetCore.Components;

namespace Pressentation_WebApp.Components;
public partial class EditServices
{
  [Parameter] public ServiceTypeDto Service { get; set; } = new();
  [Parameter] public EventCallback<ServiceTypeDto> OnConfirmed { get; set; }
  [Parameter] public EventCallback OnClose { get; set; }


  private async Task Confirm() => await OnConfirmed.InvokeAsync(Service);
  private async Task Close() => await OnClose.InvokeAsync();
}
