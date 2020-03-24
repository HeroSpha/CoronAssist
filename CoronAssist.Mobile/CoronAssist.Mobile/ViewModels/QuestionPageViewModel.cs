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
        public ICollection<SurveyAnswer> SurveyAnswers { get; set; } 
        public Personal Personal { get; set; }
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
            SurveyAnswers =  new HashSet<SurveyAnswer>();
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
                            Id = App.User.Id,
                            Fullname = Personal.Fullname,
                            PhoneNumber = Personal.PhoneNumber,
                            Address = Personal.EmailAddres,
                            EmailAddress = Personal.EmailAddres,
                            Age = Personal.Age,
                            MedicalAidNumber = Personal.MedicalAidNumber
                        })
                        .ReceiveJson<AccountSurvey>();
                       
                    if (accountSurvey != null)
                    {
                       
                        foreach (var answer in SurveyAnswers)
                        {
                            answer.AccountSurveyId = accountSurvey.AccountSurveyId;
                        }
                        var _answer = await ServerPath.Path
                            .AppendPathSegment("api/surveyanswers/addsurveyanswers")
                            .PostJsonAsync(SurveyAnswers)
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
        public void SetPersonal(Personal personal)
        {
            this.Personal = personal;
        }
        public void SetSurvey(Survey survey)
        {
            this.Survey = survey;
        }
        public async void SetPoints(int answerId)
        {
            var Availableanswer = Survey.Questions.SelectMany(ans => ans.Answers).FirstOrDefault(_answer =>_answer.AnswerId == answerId);
            var answer = Answers.FirstOrDefault(p => p.AnswerId == answerId);

            if(Availableanswer != null && Availableanswer.AnswerType == AnswerType.Yes)
            {
                var _av = SurveyAnswers.FirstOrDefault(p => p.Answer == Availableanswer.UserAnswer);
                if(_av == null)
                {
                    var flight = await SetFlight();
                    var _surveyAnswer = new SurveyAnswer
                    {
                        Answer = Availableanswer.UserAnswer,
                        Question = Availableanswer.Question.SurveyQuestion,
                        FlightDetail = new FlightDetail
                        {
                            DepartureDate = flight.DepartureDate,
                            ArrivalDate = flight.ArrivalDate,
                            FlightNumber = flight.FlightNumber
                        }
                    };
                    SurveyAnswers.Add(_surveyAnswer);
                }
                else
                {
                    SurveyAnswers.Remove(_av);
                }
               
            }
            if(answer != null)
            {
                Answers.Remove(answer);
            }
            else
            {
                Answers.Add(Availableanswer);
            }
        }
        private async Task<FlightDetail> SetFlight()
        {
            var flightDetails = new FlightDetail();
            var _departureDate = await UserDialogs.Instance.DatePromptAsync("Departure Date");
            flightDetails.DepartureDate = _departureDate.SelectedDate;
            var _arrivalDate = await UserDialogs.Instance.DatePromptAsync("Arrival Date");
            flightDetails.ArrivalDate = _arrivalDate.SelectedDate;
             var flightNumber = await UserDialogs.Instance.PromptAsync("", "Flight number", "Ok", "Cancel", "Enter flight number", InputType.Default, null);
            flightDetails.FlightNumber = flightNumber.Text;
            return flightDetails;
           
        }
        
    }
}
