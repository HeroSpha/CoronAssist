using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.Account
{
    public partial class AccountPage
    {
        [Inject]
        public IBookRepository BookRepository  { get; set; }
        public ICollection<Book> Books { get; set; }
       
        public AccountPage()
        {
            Books = new HashSet<Book>();
        }
        protected async override Task OnInitializedAsync()
        {
            Books = await BookRepository.GetBookings();
        }
    }
}
