using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Coronassist.Web.Shared.DAL;
using Coronassist.Web.Shared.DAL.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Repositories
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        public QuestionRepository(DatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            try
            {
                var q = await DatabaseService.accountContext.Questions.FirstOrDefaultAsync(p => p.QuestionId == question.QuestionId);
                if (q != null)
                {
                    q.SurveyQuestion = question.SurveyQuestion;
                    DatabaseService.accountContext.Questions.Update(q);
                    await DatabaseService.accountContext.SaveChangesAsync();
                    return q;
                }
                else
                {
                    var _question = await DatabaseService.accountContext.Questions.AddAsync(question);
                    await DatabaseService.accountContext.SaveChangesAsync();
                    return _question.Entity;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteQuestion(int QuestionId)
        {
            try
            {
                var q = await DatabaseService.accountContext.Questions.FirstOrDefaultAsync(p => p.QuestionId == QuestionId);
                if (q != null)
                {
                    DatabaseService.accountContext.Remove(q);
                    await DatabaseService.accountContext.SaveChangesAsync();
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

        public async Task<List<Question>> GetQuestionsAsync(int surveyId)
        {
            try
            {
                var questions = await DatabaseService.accountContext.Questions
                    .Include(f => f.Answers)
                    .Include(f => f.Survey)
                    .Where(p => p.SurveyId == surveyId).ToListAsync();
                return questions.Select(question => new Question
                {
                    SurveyQuestion = question.SurveyQuestion,
                    Survey = new Survey
                    {
                        Name = question.Survey.Name
                    },
                    IsActive = question.IsActive,
                    Answers = question.Answers.Select(answer => new Answer
                    {
                        UserAnswer = answer.UserAnswer,
                        IsActive = answer.IsActive,
                        Percentage = answer.Percentage
                    }).ToList(),
                    QuestionId = question.QuestionId,
                    SurveyId = question.SurveyId

                   
                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
