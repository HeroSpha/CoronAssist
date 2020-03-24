using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Coronassist.Web.Shared.DAL;
using Coronassist.Web.Shared.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class AnswerRepository : BaseRepository, IAnswerRepository
    {
        public AnswerRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<Answer> AddAnswer(Answer answer)
        {
            try
            {
                var _an = await accountDbContext.Answers.FirstOrDefaultAsync(p => p.AnswerId == answer.AnswerId);
                if(_an != null)
                {
                    _an.UserAnswer = answer.UserAnswer;
                    _an.IsActive = answer.IsActive;
                    _an.Percentage = answer.Percentage;
                    accountDbContext.Answers.Update(_an);
                    await accountDbContext.SaveChangesAsync();
                    return _an;
                }
                else
                {
                    var _answer = await accountDbContext.Answers.AddAsync(answer);
                    await accountDbContext.SaveChangesAsync();
                    return _answer.Entity;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAnswer(int AnswerId)
        {
            try
            {
                var _an = await accountDbContext.Answers.FirstOrDefaultAsync(p => p.AnswerId == AnswerId);
                if (_an != null)
                {
                    accountDbContext.Answers.Remove(_an);
                    await accountDbContext.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
