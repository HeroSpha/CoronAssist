using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using Flurl;
using Flurl.Http;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public LoginPageViewModel(INavigation _navigation) : base(_navigation)
        {
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
            var token = Xamarin.Essentials.SecureStorage.GetAsync("Token").Result;
            if(!string.IsNullOrEmpty(token))
            {
                Shell.Current.GoToAsync("//home");
                GetUser();
            }
        }
        private async void GetUser()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                var Id = Xamarin.Essentials.SecureStorage.GetAsync("Id").Result;
                App.User = await ServerPath.Path
                    .AppendPathSegment($"api/accounts/getuser/{Id}")
                    .GetJsonAsync<ApplicationUser>();
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync("Try and login again.");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        private async Task RegisterAsync()
        {
            try
            {
                await Shell.Current.GoToAsync("//login/registration");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task LoginAsync()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                var login = await ServerPath.Path
                    .AppendPathSegment("api/accounts/authenticate")
                    .PostJsonAsync(new UserDto { Email = Username, Password = Password })
                    .ReceiveJson<LoginView>();
                if(login != null)
                {
                    App.User = login.User;
                    App.Token = login.Token;
                    await Shell.Current.GoToAsync("//home");
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Invalid username or password");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
