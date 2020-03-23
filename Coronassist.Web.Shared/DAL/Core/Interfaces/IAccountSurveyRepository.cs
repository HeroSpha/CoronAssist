using ChartJs.Blazor.ChartJS.BarChart;
using ChartJs.Blazor.ChartJS.Common.Wrappers;
using Coronassist.Web.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coronassist.Web.Shared.DAL.Core.Interfaces
{
    public interface IAccountSurveyRepository
    {
        Task<AccountSurvey> AddAccountSurvey(AccountSurvey accountSurvey);
        Task<AccountSurvey> GetAccountSurvey(int accountSurveyId);
        Task<BarConfig> GetDailyReports();
        Task<(double low, double mid, double high)> GetDoughartData();
        


    }
}
