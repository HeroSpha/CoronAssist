using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class FlightDetailRepository : BaseRepository, IFlightDetailRepository
    {
        public FlightDetailRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<FlightDetail> AddFlightDetail(FlightDetail flightDetail)
        {
            try
            {
                var _flight = await accountDbContext.FlightDetails.AddAsync(flightDetail);
                await accountDbContext.SaveChangesAsync();
                return _flight.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
