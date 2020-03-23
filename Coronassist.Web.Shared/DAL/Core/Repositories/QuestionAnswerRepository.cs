using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class QuestionAnswerRepository : BaseRepository, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(DatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<bool> AddRange(List<SurveyAnswer> Answers)
        {
            DatabaseService.accountContext.QuestionAnswers.AddRange(Answers);
            await DatabaseService.accountContext.SaveChangesAsync();
            return true;
        }
    }
}
