using CoronAssist.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class ResultPageViewModel : BaseViewModel
    {

        private string backgroundColor;
        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty(ref backgroundColor, value); }
        }
        private string result;
        public string Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }
        public ResultPageViewModel(INavigation _navigation) : base(_navigation)
        {
        }
        public void SetResult(UserSurveyStatus userSurveyStatus)
        {
            switch(userSurveyStatus)
            {
                case UserSurveyStatus.Low:
                    {
                        BackgroundColor = "#08A626";
                        Result = "Low Risk";
                        Message = "You are at low risk, keep safe.";
                    }
                    break;
                case UserSurveyStatus.Midium:
                    {
                        BackgroundColor = "#EC931B";
                        Result = "Medium Risk";
                        Message = "We recommend you book appoint with the Doctor.";
                    }
                    break;
                case UserSurveyStatus.High:
                    {
                        BackgroundColor = "#FF000F";
                        Result = "Medium Risk";
                        Message = "You are at high risk, we recommend self quarantine. Request home testing.";
                    }
                    break;
            }
        }
        
    }
}
