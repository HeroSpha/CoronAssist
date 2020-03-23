using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurvayDetailToolBar
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public EventCallback DeleteSurvey { get; set; }
        [Parameter]
        public EventCallback OpenDialog { get; set; }
    }
}