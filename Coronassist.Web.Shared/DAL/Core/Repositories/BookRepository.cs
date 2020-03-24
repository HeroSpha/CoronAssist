using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<Book> AddBookings(Book accountBook)
        {
            try
            {
                var booking = await accountDbContext.Books.FirstOrDefaultAsync(p => p.AccountBookId == accountBook.AccountBookId);
                //Update
                if(booking != null)
                {
                    booking.BookDate = accountBook.BookDate;
                    booking.IsPaid = accountBook.IsPaid;
                    booking.BookingStatus = accountBook.BookingStatus;
                    return booking;
                }
                else
                {
                    //insert
                    var book = await accountDbContext.Books.AddAsync(accountBook);
                    await accountDbContext.SaveChangesAsync();
                    return book.Entity;
                }
                

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Book>> GetBookings()
        {
            var books = await accountDbContext.Books.ToListAsync();
            return books.Select(book => new Book
            {
                BookDate = book.BookDate,
                BookingStatus = book.BookingStatus,
                IsPaid = book.IsPaid,
                PatientName = book.PatientName,
                AccountBookId = book.AccountBookId,
                Id = book.Id

            }).ToList();
        }

        public async Task<int> TotalBookings()
        {
            return accountDbContext.Books.Count();
        }
    }
}
