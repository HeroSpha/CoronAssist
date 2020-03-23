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
    public class BlogDetailPageViewModel : BaseViewModel
    {
        private Blog blog;
        public Blog Blog
        {
            get { return blog; }
            set { SetProperty(ref blog, value); }
        }
        public BlogDetailPageViewModel(INavigation _navigation) : base(_navigation)
        {
        }
        public async void SetBlog(Blog blog)
        {
            try
            {
                Blog = blog;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
