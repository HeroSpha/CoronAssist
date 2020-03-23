using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Blazor.Dashboard.App.View.AlertService;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class AddSurvey
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }
        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LowUpperBound { get; set; }
        public string MidUpperBound { get; set; }
        public AddSurvey()
        {
        }
        protected async override Task OnInitializedAsync()
        {
            SweetAlertMessage.Prepare(JSRuntime);
        }

        private void HandleNameChange(ChangeEventArgs arg)
        {
            Name = arg.Value.ToString();
        }
        private void HandleMidUpperBoundChange(ChangeEventArgs arg)
        {
            MidUpperBound = arg.Value.ToString();
        }
        private void HandleLowUpperBoundChange(ChangeEventArgs arg)
        {
            LowUpperBound = arg.Value.ToString();
        }
        private void HandleDescriptionChange(ChangeEventArgs arg)
        {
            Description = arg.Value.ToString();
        }
        private async void AddSurveyAsync()
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(LowUpperBound) && !string.IsNullOrEmpty(MidUpperBound))
            {
                var survey = await SurveyRepository.AddSurvey(new Survey
                {
                    Name = Name,
                    Description = Description,
                    CreatedOn = DateTime.Now,
                    SurveyStatus = SurveyStatus.Pending,
                    LowUpperBound = double.Parse(LowUpperBound),
                    MidUpperBound = double.Parse(MidUpperBound)

                });
                if (survey != null)
                {
                    Name = string.Empty;
                    Description = string.Empty;
                    LowUpperBound = string.Empty;
                    MidUpperBound = string.Empty;
                    await SweetAlertMessage.SuccessMessage();
                }
                else
                    await SweetAlertMessage.ErrorMessage();
            }
            else
                await SweetAlertMessage.ErrorMessage();
        }
        
    }
}