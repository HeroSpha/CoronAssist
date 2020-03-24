using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class QuestionAnswerRepository : BaseRepository, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<bool> AddRange(List<SurveyAnswer> Answers)
        {
            accountDbContext.QuestionAnswers.AddRange(Answers);
            await accountDbContext.SaveChangesAsync();
            return true;
        }
    }
}
