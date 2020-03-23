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
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        public QuestionsController(IQuestionRepository _questionRepository)
        {
            questionRepository = _questionRepository;
        }
        [HttpPost("addquestion")]
        public async Task<IActionResult> AddQuestion([FromBody]Question question)
        {
            try
            {
                return Ok(await questionRepository.AddQuestionAsync(question));

            }
            catch (Exception)
            {

                throw;
            }
        }
      
    }
}