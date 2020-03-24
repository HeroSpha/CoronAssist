using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class PersonalDetailViewModel : BaseViewModel
    {
        public Survey Survey { get; set; }
        private string fullname;
        public string Fullname
        {
            get { return fullname; }
            set { SetProperty(ref fullname, value); }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { SetProperty(ref emailAddress, value); }
        }
        private string medicalAidNumber;
        public string MedicalAidNumber
        {
            get { return medicalAidNumber; }
            set { SetProperty(ref medicalAidNumber, value); }
        }
        private int age;
        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
        }
        public ICommand SubmitCommand { get; set; }
        public PersonalDetailViewModel(INavigation _navigation) : base(_navigation)
        {
            SubmitCommand = new Command(async () => await Submit());
        }
        public void SetSurvey(Survey survey)
        {
            Survey = survey;
        }
        private async Task Submit()
        {
            try
            {
              
                await Navigation.PushAsync(new QuestionPage(new Personal { Fullname = Fullname, Age = Age, EmailAddres = EmailAddress, MedicalAidNumber = MedicalAidNumber, PhoneNumber = PhoneNumber }, Survey));
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
