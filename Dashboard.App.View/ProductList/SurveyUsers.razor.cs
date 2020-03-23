using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyUsers
    {
        [Parameter]
        public IEnumerable<AccountSurvey> AccountSurveys { get; set; }
    }
}