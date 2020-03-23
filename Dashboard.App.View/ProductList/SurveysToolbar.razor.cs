using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveysToolbar
    {
        [Parameter] public EventCallback OnAddSurvey { get; set; }
    }
}
