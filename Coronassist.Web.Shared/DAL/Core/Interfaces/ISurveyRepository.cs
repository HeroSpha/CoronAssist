using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface ISurveyRepository
    {
        Task<Survey> AddSurvey(Survey survey);
        Task<bool> DeleteSurvey(int surveyId);
        Task<Survey> GetSurvey(int surveyId);
        Task<List<Survey>> GetSurveys();
       
    }
}
