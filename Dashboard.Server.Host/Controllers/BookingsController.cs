using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skclusive.Blazor.Dashboard.Server.Host.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BookingsController(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        } 
        [HttpGet("getbookings")]
        public async Task<IActionResult> GetBookings()
        {
            return Ok(await bookRepository.GetBookings());
        }
        [HttpPost("addbooking")]
        public async Task<IActionResult> AddBooking([FromBody]Book book)
        {
            return Ok(await bookRepository.AddBookings(book));
        }

       
    }
}