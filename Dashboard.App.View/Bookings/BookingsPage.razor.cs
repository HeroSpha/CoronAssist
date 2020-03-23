using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Skclusive.Blazor.Dashboard.App.View.AlertService;

namespace Skclusive.Blazor.Dashboard.App.View.Bookings
{
    public partial class BookingsPage
    {
        [Inject]
        public ISweetAlertMessage SweerAlertMessage { get; set; }
        [Inject]
        public IBookRepository BookRepository { get; set; }
        public IEnumerable<Book> Bookings { get; set; }

        public BookingsPage()
        {
            Bookings = new HashSet<Book>();
        }    
        protected async override Task OnInitializedAsync()
        {
            //Bookings = await BookRepository.GetBookings();
            Bookings = new List<Book>
            {
                new Book { Address = "123 Dresden Crescent", BookDate = DateTime.Now, BookingStatus = BookingStatus.Booked, IsPaid = true, PatientName = "Siphamandla Ngwenya"},
                new Book { Address = "123 Dresden Crescent", BookDate = DateTime.Now, BookingStatus = BookingStatus.Pending, IsPaid = true, PatientName = "Dany Ngwenya"}
            };
            StateHasChanged();
        }
        private async void ProcessBook(BookingStatus bookingStatus)
        {
            
        }
        private async void DeclineBooking(Book book)
        {
            var confirm = await SweerAlertMessage.ConfirmDialogAsync(Text: "Decline selected booking.");
            if(confirm == "Yes")
            {
                var _book = await BookRepository.AddBookings(book);
            }
        }
    }
}