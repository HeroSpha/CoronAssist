using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoronAssist.Mobile.Services;
using CoronAssist.Mobile.Views;
using CoronAssist.Mobile.Models;

namespace CoronAssist.Mobile
{
    public partial class App : Application
    {
        public static ApplicationUser User;
        public static string Token { get; set; }
        public App()
        {
            InitializeComponent();
            var token = Xamarin.Essentials.SecureStorage.GetAsync("Token").Result;
           
            MainPage = new AppShell();

            // MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
