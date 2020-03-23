using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using CoronAssist.Mobile.Views;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class QuestionPageViewModel : BaseViewModel
    {
        private double points;
        public double Points
        {
            get { return points; }
            set { SetProperty(ref points, value); 
                
            }
        }
        private ObservableCollection<Answer> answers;
        public ObservableCollection<Answer> Answers
        {
            get { return answers; }
            set { SetProperty(ref answers, value);
                
            }
        }
        private Survey survey;
        public Survey Survey
        {
            get { return survey; }
            set { SetProperty(ref survey, value); }
        }
        public QuestionPageViewModel(INavigation _navigation) : base(_navigation)
        {
            Answers = new ObservableCollection<Answer>();
            Answers.CollectionChanged += Answers_CollectionChanged;
            SubmitCommand = new Command(async() => await Submit());
        }

        public ICommand SubmitCommand { get; set; }
        private void Answers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Points = Answers.ToList<Answer>().Sum(p => p.Percentage);
        }
        private async Task Submit()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                if (Answers.Count > 0)
                {

                    //add account survey 
                    var accountSurvey = await ServerPath.Path
                        .AppendPathSegment("api/accountsurveys/addaccountsurvey")
                        .PostJsonAsync(new AccountSurvey
                        {
                            SurveyDate = DateTime.Now,
                            Risk = Answers.Sum(p => p.Percentage),
                            SurveyId = Survey.SurveyId,
                            UserSurveyStatus = GetStatus(Points),
                            Id = "f6df05cb-ca34-44ef-a13e-e26807485522" /*App.User.Id*/
                        })
                        .ReceiveJson<AccountSurvey>();
                       
                    if (accountSurvey != null)
                    {
                        var queastionAnswers = new List<SurveyAnswer>();
                        foreach (var answer in Answers)
                        {
                            queastionAnswers.Add(new SurveyAnswer
                            {
                                Answer = answer.UserAnswer,
                                AccountSurveyId = accountSurvey.AccountSurveyId,
                                Question = answer.Question.SurveyQuestion
                            });
                        }
                        var _answer = await ServerPath.Path
                            .AppendPathSegment("api/surveyanswers/addsurveyanswers")
                            .PostJsonAsync(queastionAnswers)
                            .ReceiveJson<bool>();
                        if(_answer)
                        {
                            await Navigation.PushAsync(new ResultPage(accountSurvey.UserSurveyStatus));
                        }

                    }

                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Answer at least 1 question");
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        private UserSurveyStatus GetStatus(double Points)
        {
            if (Points <= Survey.LowUpperBound)
                return UserSurveyStatus.Low;
            else if (Points <= Survey.MidUpperBound)
                return UserSurveyStatus.Midium;
            else
                return UserSurveyStatus.High;
        }
        public void SetSurvey(Survey survey)
        {
            this.Survey = survey;
        }
        public void SetPoints(int answerId)
        {
            var Availableanswer = Survey.Questions.SelectMany(ans => ans.Answers).FirstOrDefault(_answer =>_answer.AnswerId == answerId);
            var answer = Answers.FirstOrDefault(p => p.AnswerId == answerId);
            if(answer != null)
            {
                Answers.Remove(answer);
            }
            else
            {
                Answers.Add(Availableanswer);
            }
        }
        
    }
}
