using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class RegisterPageViewModel :  BaseViewModel
    {
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { SetProperty(ref fullName, value); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public RegisterPageViewModel(INavigation _navigation) : base(_navigation)
        {
            LoginCommand = new Command(async () => await Shell.Current.GoToAsync("//login"));
            SignUpCommand = new Command(async () => await SignUp());
        }
        private async Task SignUp()
        {
            try
            {
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(PhoneNumber) && !string.IsNullOrEmpty(Address))
                {
                    UserDialogs.Instance.ShowLoading();

                    var signUp = await ServerPath.Path
                        .AppendPathSegment("api/accounts/register")
                        .AllowAnyHttpStatus()
                        .PostJsonAsync(new Usermodel
                        {
                            FullAddress = Address,
                            Fullname = FullName,
                            Email = Username,
                            ConfirmPassword = ConfirmPassword,
                            Password = Password,
                            PhoneNumber = PhoneNumber,
                            Province = "Gauteng",
                            Role = "User"
                        })
                        .ReceiveJson<LoginView>();
                    if (signUp.IsSuccess)
                    {
                        App.User = signUp.User;
                        App.Token = signUp.Token;
                        await Xamarin.Essentials.SecureStorage.SetAsync("Id", signUp.User.Id);
                        await Xamarin.Essentials.SecureStorage.SetAsync("Token", signUp.Token);
                      //  await Xamarin.Essentials.SecureStorage.SetAsync("Name", signUp.User.Fullname);
                        await Shell.Current.GoToAsync("//home");
                    }

                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Missing required fields.");
                }
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
