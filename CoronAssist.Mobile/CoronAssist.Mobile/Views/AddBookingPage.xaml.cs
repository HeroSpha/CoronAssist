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
    public partial class AddBookingPage : ContentPage
    {
        AddBookigPageViewModel ViewModel;
        public AddBookingPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new AddBookigPageViewModel(Navigation);
        }
    }
}