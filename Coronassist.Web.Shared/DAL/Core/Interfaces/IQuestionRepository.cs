using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Question> AddQuestionAsync(Question question);
        Task<bool> DeleteQuestion(int QuestionId);
        Task<List<Question>> GetQuestionsAsync(int surveyId);
    }
}
