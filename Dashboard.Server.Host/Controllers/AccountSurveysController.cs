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
    [Route("api/accountsurveys")]
    [ApiController]
    public class AccountSurveysController : ControllerBase
    {
        private readonly IAccountSurveyRepository accountSurveyRepository;
        public AccountSurveysController(IAccountSurveyRepository _accountSurveyRepository)
        {
            accountSurveyRepository = _accountSurveyRepository;
        }
        [HttpPost("addaccountsurvey")]
        public async Task<ActionResult> AddAccountSurvey([FromBody]AccountSurvey accountSurvey)
        {
            var acc = await accountSurveyRepository.AddAccountSurvey(accountSurvey);
            return Ok(acc);
        }
    }
}