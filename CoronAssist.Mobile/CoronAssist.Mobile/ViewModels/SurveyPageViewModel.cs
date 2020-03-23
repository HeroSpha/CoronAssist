using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using CoronAssist.Mobile.Views;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class SurveyPageViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; set; }
        private Survey selectedSurvey;
        public Survey SelectedSurvey
        {
            get { return selectedSurvey; }
            set { SetProperty(ref selectedSurvey, value); 
                if(SelectedSurvey != null)
                {
                    Navigation.PushAsync(new SurveyDetailPage(SelectedSurvey.SurveyId), true);
                    SelectedSurvey = null;
                }
            }
        }
        private ObservableCollection<Survey> surveys;

        public SurveyPageViewModel(INavigation _navigation) : base(_navigation)
        {
            GetSurveys();
            LogOutCommand = new Command(async () => await SignOut());
        }
        private async Task SignOut()
        {
            var comfirm = await UserDialogs.Instance.ConfirmAsync("Log out of the system", "Log out", "Logout", "Cancel", null);
            if(comfirm)
            {
                Xamarin.Essentials.SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync("//login");
            }
        }
        public ObservableCollection<Survey> Surveys
        {
            get { return surveys; }
            set { SetProperty(ref surveys, value); }
        }
       
        public async void GetSurveys()
        {
            try
            {

                UserDialogs.Instance.ShowLoading();
                var _surveys = await ServerPath.Path
                .AppendPathSegment("api/surveys/getsurveys")
                .AllowHttpStatus()
                .GetJsonAsync<List<Survey>>();


                Surveys = new ObservableCollection<Survey>(_surveys);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
