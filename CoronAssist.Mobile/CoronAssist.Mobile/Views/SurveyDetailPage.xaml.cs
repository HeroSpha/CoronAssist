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
    public partial class SurveyDetailPage : ContentPage
    {
        SurveyDetailPageViewModel viewModel;
        public SurveyDetailPage(int SurveyId)
        {
            InitializeComponent();
            BindingContext = viewModel = new SurveyDetailPageViewModel(Navigation);
            viewModel.SetSurvey(SurveyId);
        }
    }
}