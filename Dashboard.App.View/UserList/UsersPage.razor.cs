using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Blazor.Dashboard.App.View.AlertService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skclusive.Blazor.Dashboard.App.View.UserList
{
    
    public partial class UsersPage
    {
        [Inject]
        public IQuestionAnswerRepository QuestionAnswerRepository { get; set; }
        [Inject]
        public IAccountSurveyRepository AccountSurveyRepository { get; set; }
        public ICollection<Answer> UserAnswers { get; set; }
        public int TotalPoints { get; set; }
        private int surveyId;
        public int SurveyId
        {
            get { return surveyId; }
            set { surveyId = value; }
        }
        public Survey Survey { get; set; } = new Survey();
        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }

        [Inject]
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Survey> Surveys { get; set; }
        public UsersPage()
        {
            Users = new HashSet<ApplicationUser>();
            Surveys = new HashSet<Survey>();
            UserAnswers = new HashSet<Answer>();
        } 
        private void AddUser()
        {
            NavigationManager.NavigateTo("uses/allusers/adduser");
        }
        protected async override Task OnInitializedAsync()
        {
            SweetAlertMessage.Prepare(JSRuntime);
            Users = await ApplicationUserRepository.GetAll();
        }
        private async void DeleteUser(ApplicationUser applicationUser)
        {
            try
            {
                var confirm = await SweetAlertMessage.ConfirmDialogAsync(Text: $"Delete {applicationUser.Fullname}", ConfirmButtonText: "Delete");
                if(confirm == "Yes")
                {
                    var deleted = await ApplicationUserRepository.DeleteUser(applicationUser.Id);
                    if(deleted)
                    {
                        Users.Remove(applicationUser);
                        StateHasChanged();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Open { get; set; }
        public string UserId { get; set; }
        private async void TakeSurvey(string Id)
        {
            UserId = Id;
            Surveys = new HashSet<Survey>();
            var confirm = await SweetAlertMessage.ConfirmDialogAsync(Title: "Take Survey", Text: "Choose any available survey.");
            if (confirm == "Yes")
            {
                Surveys = await SurveyRepository.GetSurveys();
                Survey = await SurveyRepository.GetSurvey(Surveys.FirstOrDefault().SurveyId);
                Open = true;
                StateHasChanged();
            }

        }
        private void OnClose()
        {
            Open = false;
            UserId = string.Empty;
        }
       
        private void OnChange(Answer answer)
        {
            var IsChecked = UserAnswers.FirstOrDefault(p => p.AnswerId == answer.AnswerId);
            if(IsChecked == null)
            {
                UserAnswers.Add(answer);
            }
            else
            {
                UserAnswers.Remove(IsChecked);
            }
            StateHasChanged();
        }
        private async void SubmitSurvey()
        {
            var answers = new List<SurveyAnswer>();
            if(UserAnswers.Count > 0 && !string.IsNullOrEmpty(UserId))
            {
                //Submit UserSurvey
                var userSurvey = await AccountSurveyRepository.AddAccountSurvey(new AccountSurvey
                {
                    Id = UserId,
                    SurveyId = Survey.SurveyId,
                    Risk = UserAnswers.Sum(p => p.Percentage),
                    SurveyDate = DateTime.Now,
                    UserSurveyStatus = SurveyResult(UserAnswers.Sum(p => p.Percentage))
                });
                if(userSurvey != null)
                {
                    foreach (var userAnswer in UserAnswers)
                    {
                        answers.Add(new SurveyAnswer
                        {
                            
                            AccountSurveyId = userSurvey.AccountSurveyId,
                            Answer = userAnswer.UserAnswer,
                            Question = userAnswer.Question.SurveyQuestion
                        });
                    }
                   var added = await QuestionAnswerRepository.AddRange(answers);
                    if(added)
                    {
                        
                        Open = false;
                        StateHasChanged();
                        await SweetAlertMessage.SuccessMessage();
                        //Navigate to show results
                    }
                    else
                    {
                        await SweetAlertMessage.ErrorMessage();
                    }
                }
                
            }
            else
            {
                await SweetAlertMessage.ErrorMessage(Text: "Survey not completed, please answer atleast one question");
            
            }
        }
        private UserSurveyStatus SurveyResult(double Points)
        {
            if (Points <= Survey.LowUpperBound)
                return UserSurveyStatus.Low;
            else if (Points <= Survey.MidUpperBound)
                return UserSurveyStatus.Midium;
            else
                return UserSurveyStatus.High;
        }
       
    }
}
