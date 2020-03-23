using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyUsersPanel
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public IEnumerable<AccountSurvey> AccountSurveys { get; set; }
        private void ViewAccountSurvey(int accountSurveyId)
        {
            NavigationManager.NavigateTo($"surveys/surveydetail/accountsurvey/{accountSurveyId}");
        }
        
    }
}