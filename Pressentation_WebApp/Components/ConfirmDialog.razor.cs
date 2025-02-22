﻿using Microsoft.AspNetCore.Components;

namespace Pressentation_WebApp.Components;
public partial class ConfirmDialog
{
  [Parameter] public string Title { get; set; } = "Bekräfta borttagning";
  [Parameter] public string Message { get; set; } = "Är du säker på att du vill ta bort detta projekt?";
  [Parameter] public EventCallback<bool> OnConfirmed { get; set; }
  [Parameter] public ModelType ModelType { get; set; } = ModelType.Confirmation;
  [Parameter] public bool Show { get; set; } = false;

  private async Task Confirm() => await OnConfirmed.InvokeAsync(true);
  private async Task Close() => await OnConfirmed.InvokeAsync(false);
}
