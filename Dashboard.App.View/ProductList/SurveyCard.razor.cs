using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyCard
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public Survey Survey { set; get; }
        private void ViewSurvey(int surveyId)
        {
            NavigationManager.NavigateTo($"/surveys/allsurveys/surveydetail/{surveyId}");
        }
    }
}
