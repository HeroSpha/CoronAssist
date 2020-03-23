using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skclusive.Blazor.Dashboard.BrowserPrerendered.Host.Controllers
{
    [Route("api/surveys")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyRepository surveyRepository;
        public SurveysController(ISurveyRepository _surveyRepository)
        {
            surveyRepository = _surveyRepository;
        }
        [HttpGet("getsurveys")]
        public async Task<IActionResult> GetSurveys()
        {
            return Ok(await surveyRepository.GetSurveys());
        }
        [HttpPost("addsurvey")]
        public async Task<IActionResult> AddSurvey([FromBody]Survey survey)
        {
            return Ok(await surveyRepository.AddSurvey(survey));
        }
        [HttpGet("getsurvey/{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId)
        {
            return Ok(await surveyRepository.GetSurvey(surveyId));
        }
    }
}