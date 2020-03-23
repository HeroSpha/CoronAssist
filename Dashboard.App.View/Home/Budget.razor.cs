using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Home
{
    public partial class Budget
    {
        public int TotalUsers { get; set; } = 0;
        [Inject]
        public IBookRepository BookRepository { get; set; }
        protected async override Task OnInitializedAsync()
        {
            TotalUsers = await BookRepository.TotalBookings();
        }

    }
}
