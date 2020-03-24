using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoronAssist.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalDetail : ContentPage
    {
        PersonalDetailViewModel ViewModel;
        public PersonalDetail(Survey survey)
        {
            InitializeComponent();
            BindingContext = ViewModel = new PersonalDetailViewModel(Navigation);
            ViewModel.SetSurvey(survey);
        }
    }
}