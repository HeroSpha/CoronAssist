using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyPanel
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public IEnumerable<Survey> Surveys { set; get; }
       
       
        
       
    }
}
