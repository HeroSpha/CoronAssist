using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Account
{
    public partial class AccountSurveyPage
    {
        public ApplicationUser User { get; set; } = new ApplicationUser();
        [Inject]
        public IAccountSurveyRepository AccountSurveyRepository { get; set; }
        [Parameter]
        public int AccountSurveyId { get; set; }
        public AccountSurvey AccountSurvey { get; set; } = new AccountSurvey();
        protected async override Task OnInitializedAsync()
        {
            AccountSurvey = await AccountSurveyRepository.GetAccountSurvey(AccountSurveyId);
            if(AccountSurvey != null)
            {
                User = AccountSurvey.Account;
                StateHasChanged();
            }
        }
    }
}