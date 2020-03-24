using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using CoronAssist.Mobile.Views;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class SurveyDetailPageViewModel : BaseViewModel
    {
        private Survey survey;
        public Survey Survey
        {
            get { return survey; }
            set { SetProperty(ref survey, value); }
        }

        public object UserDialog { get; private set; }
        public ICommand TakeTestCommand { get; set; }
        public SurveyDetailPageViewModel(INavigation _navigation) : base(_navigation)
        {
            TakeTestCommand = new Command(() => { Navigation.PushAsync(new PersonalDetail(Survey), true); });
        }
        public async void SetSurvey(int surveyId)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Survey = await ServerPath.Path
                    .AppendPathSegment($"api/surveys/getsurvey/{surveyId}")
                    .GetJsonAsync<Survey>();
            }
            catch (Exception)
            {

               
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
