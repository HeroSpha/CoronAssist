using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.AlertService
{
    public class SweetAlertMessage : ISweetAlertMessage
    {
        public IJSRuntime JSRuntime { get; set; }
        public SweetAlertMessage Prepare(IJSRuntime runtime)
        {
            JSRuntime = runtime;
            return this;
        }
        public async Task<string> ConfirmDialogAsync(string Title = "Are you sure?", string Text = "You won't be able to revert this!",
            string Icon = "warning", bool ShowCancelButton = true, string ConfirmButtonColor = "#3085d6", string CancelButtonColor = "#d33", string ConfirmButtonText = "Yes", string CancelButtonText = "No", bool ReverseButtons = false)
        {
            var result = await JSRuntime.InvokeAsync<string>("confirmDialog", new { Title, Text, Icon, ShowCancelButton, ConfirmButtonColor, CancelButtonColor, ConfirmButtonText, CancelButtonText, ReverseButtons });
            return result;
        }

        public Task<string> InputTextDialogAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> InputDialog(string PlaceholderText = "Placeholer", string InputType = "text", bool ShowCancelButton = true)
        {
            var result = await JSRuntime.InvokeAsync<string>("inputDialog", new { PlaceholderText, InputType, ShowCancelButton });
            return result;
        }

        public async Task<string> SuccessMessage(string Position = "top-end", string Icon = "success", string Title = "Success", bool ShowConfirmButton = false, int Timer = 1500)
        {
            var result = await JSRuntime.InvokeAsync<string>("successDialog", new { Position, Icon, Title, ShowConfirmButton, Timer });
            return result;
        }

        public async Task<string> ErrorMessage(string Icon = "error", string Title = "Oops...", string Text = "Something went wrong!", string FooterMessage = "")
        {
            var result = await JSRuntime.InvokeAsync<string>("errorMessage", new { Icon, Title, Text, FooterMessage });
            return result;
        }

        public async Task<string> RiskWeight()
        {
            var percentage = await JSRuntime.InvokeAsync<string>("riskWeight", null);
            return percentage;
        }
    }
}
