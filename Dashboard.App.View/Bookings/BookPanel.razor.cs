using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Blazor.Dashboard.App.View.AlertService;

namespace Skclusive.Blazor.Dashboard.App.View.Bookings
{
    public partial class BookPanel
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }
        [Inject]
        public IBookRepository BookingRepository { get; set; }
        [Parameter]
        public IEnumerable<Book> Bookings { get; set; }
        public BookPanel()
        {
            
        }
        
        protected async override Task OnInitializedAsync()
        {
            SweetAlertMessage.Prepare(JSRuntime);
        }
        private async void DeclineBooking(Book book)
        {
            var confirm = await SweetAlertMessage.ConfirmDialogAsync("Update selected Booking");
            if(confirm == "Yes")
            {
                switch (book.BookingStatus)
                {
                    case BookingStatus.Booked:
                        {
                            book.BookingStatus = BookingStatus.Declined;
                            await UpdateBook(book);
                        }
                        break;
                    case BookingStatus.Declined:
                        {
                            book.BookingStatus = BookingStatus.Booked;
                            await UpdateBook(book);
                        }
                        break;

                }
            }
           
           

        }
        private async Task UpdateBook(Book book)
        {
            var _book = await BookingRepository.AddBookings(book);
            var selectedBook = Bookings.FirstOrDefault(p => p.BookId == book.BookId);
        }
        private async void ProcessBook(Book book)
        {

        }
    }
}