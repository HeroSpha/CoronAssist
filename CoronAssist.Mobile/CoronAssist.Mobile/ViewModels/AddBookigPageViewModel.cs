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
        private string patientName;
        public string PatientName
        {
            get { return patientName; }
            set { SetProperty(ref patientName, value); }
        }
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
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { SetProperty(ref emailAddress, value); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
        private DateTime minDate;
        public DateTime MinDate
        {
            get { return minDate; }
            set { SetProperty(ref minDate, value); }
        }
        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
        public ICommand SubmitCommand { get; set; }
        public AddBookigPageViewModel(INavigation _navigation) : base(_navigation)
        {
            SubmitCommand = new Command(async () => await SaveBooking());
            BookDate = DateTime.Now;
            MinDate = DateTime.Now;
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
                    BookDate = BookDate,
                    PatientName = PatientName,
                    BookingType = BookingType.Hospital,
                    ConsultationFee = 500,
                    EmailAddress = EmailAddress,
                    PhoneNumber = PhoneNumber,
                    Time = Time.ToString()
                }).ReceiveJson<Book>();
                if(_book != null)
                {
                    await Shell.Current.GoToAsync("//booking");
                }
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync("Failed to book apointment.");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
