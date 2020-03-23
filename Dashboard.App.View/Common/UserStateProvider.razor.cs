using System;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Common
{
    public partial class UserStateProvider
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public ApplicationUser User { get; set; }
    }
}