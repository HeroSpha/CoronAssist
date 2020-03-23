using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.UserList
{
    public partial class UsersTable
    {
        [Parameter]
        public IEnumerable<ApplicationUser> Users { set; get; }
        [Parameter]
        public EventCallback<ApplicationUser> DeleteUser { get; set; }
        [Parameter]
        public EventCallback<string> TakeSurvey { get; set; }
       
    }
}
