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
    public class AddBookigPageViewModel : BaseViewModel
    {
        private string address;
        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }
        private DateTime bookDate;
        public DateTime BookDate
        {
            get { return bookDate; }
            set { SetProperty(ref bookDate, value); }
        }
        public ICommand SubmitCommand { get; set; }
        public AddBookigPageViewModel(INavigation _navigation) : base(_navigation)
        {
            SubmitCommand = new Command(async () => await SaveBooking());
        }
        private async Task SaveBooking()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                var _book = await ServerPath.Path
                .AppendPathSegment("api/booking/addbooking")
                .PostJsonAsync(new Book
                {
                    IsPaid = true,
                    BookingStatus = BookingStatus.Booked,
                    Address = Address,
                    CreatedOn = DateTime.Now,
                    Id = App.User.Id,
                    BookDate = BookDate
                }).ReceiveJson<Book>();
                if(_book != null)
                {
                    await Shell.Current.GoToAsync("//booking");
                }
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
