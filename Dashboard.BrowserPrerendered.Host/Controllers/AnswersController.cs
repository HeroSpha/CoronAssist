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
    [Route("api/answers")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerRepository answerRepository;
        public AnswersController(IAnswerRepository _answerRepository)
        {
            answerRepository = _answerRepository;
        }
        [HttpPost("addanswer")]
        public async Task<IActionResult> AddAnswer(Answer answer)
        {
            return Ok(await answerRepository.AddAnswer(answer));
        }
    }
}