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
    public partial class QuestionPage : ContentPage
    {
        QuestionPageViewModel viewModel;
        public QuestionPage(Survey survey)
        {
            InitializeComponent();
            BindingContext = viewModel = new QuestionPageViewModel(Navigation);
            viewModel.SetSurvey(survey);
        }

        private void ckAnswer_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            viewModel.SetPoints(int.Parse(((CheckBox)sender).AutomationId));
        }
    }
}