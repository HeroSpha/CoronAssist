using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.Account
{
    public partial class AccountProfile
    {
        [Parameter]
        public ApplicationUser User { get; set; }
    }
}
