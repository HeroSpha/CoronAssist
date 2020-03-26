using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
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
    public class AddBookigPageViewModel : BaseViewModel
    {
        private ObservableCollection<BookType> types;
        public ObservableCollection<BookType> BookingTypes
        {
            get { return types; }
            set { SetProperty(ref types, value); }
        }
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
        private BookType bookType;
        public BookType BookType
        {
            get { return bookType; }
            set { SetProperty(ref bookType, value); }
        }
        public ICommand SubmitCommand { get; set; }
        public AddBookigPageViewModel(INavigation _navigation) : base(_navigation)
        {
            SubmitCommand = new Command(async () => await SaveBooking());
            BookDate = DateTime.Now;
            MinDate = DateTime.Now;
            BookingTypes = new ObservableCollection<BookType>
            {
                new BookType
                {
                    BookingType = BookingType.Mobile,
                    Name = "Home"
                },
                 new BookType
                {
                    BookingType = BookingType.Hospital,
                    Name = "Hospital"
                },
            };

        }
        private async Task SaveBooking()
        {
            try
            {
                
                UserDialogs.Instance.ShowLoading();
                var _book = await ServerPath.Path
                .AppendPathSegment("api/bookings/addbooking")
                .PostJsonAsync(new Book
                {
                    IsPaid = true,
                    BookingStatus = BookingStatus.Booked,
                    Address = Address,
                    CreatedOn = DateTime.Now,
                    Id = App.User.Id,
                    BookDate = BookDate,
                    PatientName = PatientName,
                    BookingType = BookType.BookingType,
                    ConsultationFee = 500,
                    EmailAddress = EmailAddress,
                    PhoneNumber = PhoneNumber,
                    Time = Time
                }).ReceiveJson<Book>();
                if(_book != null)
                {
                    await Shell.Current.GoToAsync("//booking");
                }
            }
            catch (Exception ex)
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
