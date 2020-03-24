using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Skclusive.Blazor.Dashboard.App.View.AlertService;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyDetailQuestions
    {
        public Survey Survey { get; set; } = new Survey();
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }
        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        public string UserId { get; set; }
        public bool Open { get; set; }
        [Parameter]
        public IEnumerable<Question> Questions { get; set; }
        [Parameter]
        public EventCallback<int> DeleteQuestion { get; set; }
        [Parameter]
        public EventCallback<int> AddQuestionAsnwer { get; set; }
        [Parameter]
        public EventCallback<int> AddQuestionDetailAsnwer { get; set; }
        [Parameter]
        public EventCallback<Question> OpenQuestionAnswers { get; set; }
        private void OnClose()
        {
            Open = false;
            StateHasChanged();
        }
        public ICollection<Survey> Surveys { get; set; }
        private async void TakeSurvey(string Id)
        {
            Surveys = new HashSet<Survey>();
            var confirm = await SweetAlertMessage.ConfirmDialogAsync(Title: "Take Survey", Text: "Choose any available survey.");
            if(confirm == "Yes")
            {
                Surveys = await SurveyRepository.GetSurveys();
                Open = true;
            }
            
        }
    }
}