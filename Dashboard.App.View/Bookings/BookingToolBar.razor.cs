using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Bookings
{
    public partial class BookingToolBar
    {
        private readonly HttpClient Http;

        public BookingToolBar()
        {
        }

        public BookingToolBar(HttpClient httpClient) : this()
        {
            Http = httpClient;
        }
    }
}