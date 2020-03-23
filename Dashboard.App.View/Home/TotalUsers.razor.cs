using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Home
{
    public partial class TotalUsers
    {
        public int Users { get; set; }
        [Inject]
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Users = await ApplicationUserRepository.GetUsersCount();
        }
    }
}
