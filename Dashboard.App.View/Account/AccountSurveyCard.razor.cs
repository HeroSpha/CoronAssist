using System;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Account
{
    public partial class AccountSurveyCard
    {
        [Parameter]
        public AccountSurvey AccountSurvey { get; set; }
    }
}