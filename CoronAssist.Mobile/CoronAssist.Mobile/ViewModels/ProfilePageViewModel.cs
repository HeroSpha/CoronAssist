using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; set; }
        private ApplicationUser profile;
        public ApplicationUser Profile
        {
            get { return profile; }
            set { SetProperty(ref profile, value); }
        }
        public ProfilePageViewModel(INavigation _navigation) : base(_navigation)
        {
            LogOutCommand = new Command(async () => await Logot());
            Profile = App.User;
        }
        private async Task Logot()
        {
            var confirm = await UserDialogs.Instance.ConfirmAsync("Logout of the system?", "Logout", "Logout", "Cancel");
            if(confirm)
            {
                await Shell.Current.GoToAsync("//login");
            }
           
        }
    }
}
