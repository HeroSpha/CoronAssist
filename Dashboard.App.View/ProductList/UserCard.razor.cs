using System;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class UserCard
    {
        [Parameter]
        public AccountSurvey AccountSurvey { get; set; }
        [Parameter]
        public EventCallback<int> ViewAccountSurvey { get; set; }
    }
}