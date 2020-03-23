using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skclusive.Blazor.Dashboard.Server.Host.Controllers
{
    [Route("api/faqs")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private IFaqRepository FaqRepository;
        public FaqsController(IFaqRepository faqRepository)
        {
            FaqRepository = faqRepository;
        }
        [HttpGet("getfaq")]
        public async Task<IActionResult> GetFaq()
        {
            try
            {
                return Ok(await FaqRepository.GetFaq());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}