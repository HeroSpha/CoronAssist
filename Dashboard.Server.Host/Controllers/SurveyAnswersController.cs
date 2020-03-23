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
    [Route("api/surveyanswers")]
    [ApiController]
    public class SurveyAnswersController : ControllerBase
    {
        public readonly IQuestionAnswerRepository questionAnswerRepository;
        public SurveyAnswersController(IQuestionAnswerRepository _questionAnswerRepository)
        {
            questionAnswerRepository = _questionAnswerRepository;
        }
        [HttpPost("addsurveyanswers")]
        public async Task<IActionResult> AddSurveyAnswers([FromBody] List<SurveyAnswer> surveyAnswers)
        {
            try
            {
                return Ok(await questionAnswerRepository.AddRange(surveyAnswers));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}