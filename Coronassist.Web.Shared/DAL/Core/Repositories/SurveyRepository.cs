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
    public class SurveyRepository : BaseRepository, ISurveyRepository
    {
        public SurveyRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        public async Task<Survey> AddSurvey(Survey survey)
        {
            var _survey = await accountDbContext.Surveys.FirstOrDefaultAsync(p => p.SurveyId == survey.SurveyId);
            if(_survey != null)
            {
                _survey.Description = survey.Description;
                _survey.Name = survey.Name;
                accountDbContext.Surveys.Update(_survey);
                await accountDbContext.SaveChangesAsync();
                return _survey;
            }
            else
            {
                var sur = await accountDbContext.Surveys.AddAsync(survey);
                 await accountDbContext.SaveChangesAsync();
                return sur.Entity;
            }
        }

        public async Task<bool> DeleteSurvey(int surveyId)
        {
            try
            {
                var _survey = await accountDbContext.Surveys.FirstOrDefaultAsync(p => p.SurveyId == surveyId);
                if (_survey == null)
                    return false;
                accountDbContext.Surveys.Remove(_survey);
                await accountDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public async Task<Survey> GetSurvey(int SurveyId)
        {
            try
            {
                var survey = await accountDbContext.Surveys
                    .Include(f => f.Questions).ThenInclude(f => f.Answers)
                    .Include(f => f.UserSurveys).ThenInclude(f => f.Account)
                    .FirstOrDefaultAsync(p => p.SurveyId == SurveyId);
                return new Survey
                {
                    Name = survey.Name,
                    Description = survey.Description,
                    SurveyId = survey.SurveyId,
                    CreatedOn = survey.CreatedOn,
                    LowUpperBound = survey.LowUpperBound,
                    MidUpperBound = survey.MidUpperBound,
                    SurveyStatus = survey.SurveyStatus,
                    Questions = survey.Questions.Select(question => new Question
                    {
                        SurveyQuestion = question.SurveyQuestion,
                        QuestionId = question.QuestionId,
                        Answers = question.Answers.Select(answer => new Answer
                        {
                            AnswerId = answer.AnswerId,
                            UserAnswer = answer.UserAnswer,
                            IsActive = answer.IsActive,
                            AnswerType = answer.AnswerType,
                            Percentage = answer.Percentage,
                            QuestionId = answer.QuestionId,
                            Question = new Question
                            {
                                SurveyQuestion = question.SurveyQuestion
                            }
                        }).ToList()
                    }).ToList(),
                  UserSurveys = survey.UserSurveys.Select(user => new AccountSurvey
                  {
                      SurveyId = user.SurveyId,
                      AccountSurveyId = user.AccountSurveyId,
                      Account = new ApplicationUser
                      {
                          Fullname = user.Account.Fullname,
                          PhoneNumber = user.Account.PhoneNumber
                      },
                      SurveyDate = user.SurveyDate,
                      Risk = user.Risk,
                      UserSurveyStatus = user.UserSurveyStatus
                  }).ToList()
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Survey>> GetSurveys()
        {
            var surveys = await accountDbContext.Surveys.ToListAsync();
            return surveys.Select(survey => new Survey
            {
                Name = survey.Name,
                Description = survey.Description,
                SurveyId = survey.SurveyId,
                CreatedOn = survey.CreatedOn,
                SurveyStatus = survey.SurveyStatus,
                Questions = survey.Questions.Count > 0 ? survey.Questions.Select(question => new Question
                {

                }).ToList() : new List<Question>()
            }).ToList();
        }
    }
}
