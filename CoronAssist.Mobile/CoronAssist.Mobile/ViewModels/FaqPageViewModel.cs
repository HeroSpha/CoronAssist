using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class FaqPageViewModel : BaseViewModel
    {
        private Faq faq;
        public Faq Faq
        {
            get { return faq; }
            set { SetProperty(ref faq, value); }
        }
        public FaqPageViewModel(INavigation _navigation) : base(_navigation)
        {
           
            GetFaq();
        }
        private async void GetFaq()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Faq = await ServerPath.Path
                    .AppendPathSegment("api/faqs/getfaq")
                    .GetJsonAsync<Faq>();
            }
            catch (Exception)
            {

                
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
