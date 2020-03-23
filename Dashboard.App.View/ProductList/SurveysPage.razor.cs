using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveysPage
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        [Parameter]
        public ICollection<Survey> Surveys { get; set; }

        public SurveysPage()
        {
            Surveys = new HashSet<Survey>();
        }
        protected async override Task OnInitializedAsync()
        {
            Surveys = await SurveyRepository.GetSurveys();
            this.StateHasChanged();
        }
        private async void AddSurvey()
        {
            NavigationManager.NavigateTo("surveys/allsurveys/addsurvey");
        }
        
    }
}
