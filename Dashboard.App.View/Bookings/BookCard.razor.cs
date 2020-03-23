using System;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Bookings
{
    public partial class BookCard
    {
        [Parameter]
        public Book Book { get; set; }
        [Parameter]
        public EventCallback<BookingStatus> ProcessBooking { get; set; }
        [Parameter]
        public EventCallback<Book> DeclineBooking { get; set; }
        private string GetStatusText(BookingStatus bookingStatus)
        {
            var title = "";
            switch(bookingStatus)
            {
                case BookingStatus.Booked:
                    {
                        title = "Cancel";
                    }
                    break;
                case BookingStatus.Cancelled:
                    {
                        title = "Confirm";
                    }
                    break;
                case BookingStatus.Confirmed:
                    {
                        title = "Cancel";
                    }
                    break;
                case BookingStatus.Declined:
                    {
                        title = "Confirm";
                    }
                    break;
                case BookingStatus.Pending:
                    {
                        title = "Confirm";
                    }
                    break;
            }
            return title;

        }
    }
}