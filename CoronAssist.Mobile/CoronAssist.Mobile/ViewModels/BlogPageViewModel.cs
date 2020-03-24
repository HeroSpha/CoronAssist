using Acr.UserDialogs;
using CoronAssist.Mobile.Models;
using CoronAssist.Mobile.Services;
using CoronAssist.Mobile.Views;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CoronAssist.Mobile.ViewModels
{
    public class BlogPageViewModel : BaseViewModel
    {
        private Blog blog;
        public Blog Blog
        {
            get { return blog; }
            set { SetProperty(ref blog, value);
                if (Blog != null)
                {
                    Navigation.PushAsync(new BlogDetailPage(Blog));
                    Blog = null;
                }
            }
        }
        private ObservableCollection<Blog> blogs;

        public BlogPageViewModel(INavigation _navigation) : base(_navigation)
        {
            GetBlogs();
        }

        public ObservableCollection<Blog> Blogs
        {
            get { return blogs; }
            set { SetProperty(ref blogs, value); }
        }
      
        private async void GetBlogs()
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Blogs = new ObservableCollection<Blog>(await ServerPath.Path.AppendPathSegment("api/blogs/getblogs").AllowAnyHttpStatus().GetJsonAsync<List<Blog>>());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
