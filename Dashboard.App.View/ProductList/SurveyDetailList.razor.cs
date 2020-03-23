using System;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyDetailList
    {
       [Parameter]
       public Survey Survey { get; set; }

    }
}