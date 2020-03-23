using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Blazor.Dashboard.App.View.AlertService;
using Skclusive.Material.Core;

namespace Skclusive.Blazor.Dashboard.App.View.ProductList
{
    public partial class SurveyDetail
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
      
        private bool Open { set; get; }

        private string Text => (Open ? "Close" : "Open") + " FullScreen Dialog";
        [Inject]
        public IAnswerRepository AnswerRepository { get; set; }
        [Inject]
        public ISurveyRepository SurveyRepository { get; set; }
        [Inject]
        public IQuestionRepository QuestionRepository { get; set; }
        [Parameter]
        public int SurveyId { get; set; }

        public Survey Survey { get; set; } = new Survey();
        protected async override Task OnInitializedAsync()
        {
            SweetAlertMessage.Prepare(JSRuntime);
            Answers = new HashSet<Answer>();
            Survey = await SurveyRepository.GetSurvey(SurveyId);
        }
        private void OnClose()
        {
            Open = false;
            StateHasChanged();
        }
       
        private void OpenQuestionAnswers(Question question)
        {
            Open = true;
            Answers = question.Answers;
            StateHasChanged();
        }

        private async void SubmitQuestion()
        {
           var _question = await SweetAlertMessage.InputDialog(type: "textarea",PlaceholderText: "Type your survey question here....");
            if(!string.IsNullOrEmpty(_question))
            {
                var question = await QuestionRepository.AddQuestionAsync(new Coronassist.Web.Shared.Models.Question
                {
                    SurveyQuestion = _question,
                    IsActive = true,
                    SurveyId = SurveyId
                });
               
                this.Survey.Questions.Add(question);
                Open = false;
                StateHasChanged();
            }

           
        }
        
        private async void DeleteSurvey()
        {
            var confirm = await SweetAlertMessage.ConfirmDialogAsync(Text: "Delete this survey");
            if(confirm == "Yes")
            {
                var deleted = await SurveyRepository.DeleteSurvey(Survey.SurveyId);
                if(deleted)
                {
                    NavigationManager.NavigateTo("surveys");
                }
            }
        }
        private async void AddAnswer(int QuestionId)
        {
            var answer = await SweetAlertMessage.InputDialog(type: "textarea", PlaceholderText: "Type your survey answer here....");
            if(!string.IsNullOrEmpty(answer))
            {
                var percentage = await SweetAlertMessage.RiskWeight();
                var confirm = await SweetAlertMessage.ConfirmDialogAsync(Text: "Add this answe to the question.");
                if(confirm != null)
                {
                    var _answer = await AnswerRepository.AddAnswer(new Answer
                    {
                        QuestionId = QuestionId,
                        IsActive = true,
                        Percentage = double.Parse(percentage),
                        UserAnswer = answer
                    });

                    if (_answer != null)
                    {
                        var _updatedQuestion = Survey.Questions.FirstOrDefault(p => p.QuestionId == QuestionId);
                        _updatedQuestion.Answers.Add(_answer);
                        StateHasChanged();
                        await SweetAlertMessage.SuccessMessage();

                    }
                    else
                        await SweetAlertMessage.ErrorMessage();
                }
            }
        }
        private async void DeleteQuestion(int QuestionId)
        {

            var confirm = await SweetAlertMessage.ConfirmDialogAsync(Text: "Delete selected question");
            if(confirm == "Yes")
            {
               var deleted = await QuestionRepository.DeleteQuestion(QuestionId);
                if(deleted)
                {
                    var deleteItem = Survey.Questions.FirstOrDefault(p => p.QuestionId == QuestionId);
                    Survey.Questions.Remove(deleteItem);
                    await SweetAlertMessage.SuccessMessage();
                    StateHasChanged();
                }
                else
                {
                    await SweetAlertMessage.ErrorMessage();
                }
            }
        }
        
       
    }
}