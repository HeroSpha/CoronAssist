using Microsoft.AspNetCore.Components;
using Skclusive.Blazor.Dashboard.App.View.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.Layout
{
    public partial class FullLayout
    {
        [CascadingParameter]
        public UserStateProvider State { get; set; }
    }
}
