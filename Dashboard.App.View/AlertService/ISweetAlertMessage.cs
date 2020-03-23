using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.AlertService
{
    public interface ISweetAlertMessage
    {
        Task<string> ErrorMessage(string Icon = "error", string Title = "Oops...", string Text = "Something went wrong!", string FooterMessage = "");
        Task<string> SuccessMessage(string Position = "top-end", string Icon = "success", string Title = "Success", bool ShowConfirmButton = false, int Timer = 1500);
        Task<string> InputDialog(string PlaceholderText = "Placeholer", string type = "text", bool ShowCancelButton = true);
        Task<string> ConfirmDialogAsync(string Title = "Are you sure?", string Text = "You won't be able to revert this!",
            string Icon = "warning", bool ShowCancelButton = true, string ConfirmButtonColor = "#3085d6", string CancelButtonColor = "#d33", string ConfirmButtonText = "Yes", string CancelButtonText = "No", bool ReverseButtons = false);
        Task<string> InputTextDialogAsync();
        SweetAlertMessage Prepare(IJSRuntime iJSRuntime);
        Task<string> RiskWeight();
    }
}
