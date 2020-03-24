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
    [Route("api/flightdetails")]
    [ApiController]
    public class FlightDetailsController : ControllerBase
    {
        private IFlightDetailRepository FlightDetailRepository;
        public FlightDetailsController(IFlightDetailRepository flightDetailRepository)
        {
            FlightDetailRepository = flightDetailRepository;
        }
        [HttpPost("addflightdetail")]
        public async Task<IActionResult> AddFligtDetails([FromBody] FlightDetail flightDetail)
        {
            return Ok(await FlightDetailRepository.AddFlightDetail(flightDetail));
        }
    }
}