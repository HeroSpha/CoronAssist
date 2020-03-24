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
    public class BookingPageViewModel : BaseViewModel
    {
        private ObservableCollection<Book> bookings;
        public ObservableCollection<Book> Bookings
        {
            get { return bookings; }
            set { SetProperty(ref bookings, value); }
        }
        public ICommand AddBookingCommand { get; set; }
        public BookingPageViewModel(INavigation _navigation) : base(_navigation)
        {
            AddBookingCommand = new Command(async () => await AddBooking());
            var token = Xamarin.Essentials.SecureStorage.GetAsync("Token").Result;
            if (!string.IsNullOrEmpty(token))
            {
                Shell.Current.GoToAsync("//home");
            }
        }
        private async Task AddBooking()
        {
            await Navigation.PushAsync(new AddBookingPage());
        }
        public async void GetBookings(string Id)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Bookings = new ObservableCollection<Book>(await ServerPath.Path
               .AppendPathSegment($"api/bookings/getuserbookings/{Id}")
               .GetJsonAsync<List<Book>>());
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
