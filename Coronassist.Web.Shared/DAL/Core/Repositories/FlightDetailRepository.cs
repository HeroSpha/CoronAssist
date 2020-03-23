using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class FlightDetailRepository : BaseRepository, IFlightDetailRepository
    {
        public FlightDetailRepository(DatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<FlightDetail> AddFlightDetail(FlightDetail flightDetail)
        {
            try
            {
                var _flight = await DatabaseService.accountContext.FlightDetails.AddAsync(flightDetail);
                await DatabaseService.accountContext.SaveChangesAsync();
                return _flight.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
