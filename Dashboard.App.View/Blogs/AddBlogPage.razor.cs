using System;
using System.Linq;
using System.Net.Http;
using Blazored.TextEditor;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Blogs
{
    public partial class AddBlogPage
    {
        public string Title { get; set; }
        [Inject]
        public IBlogRepository BlogRepository { get; set; }
        BlazoredTextEditor QuillHtml;
        string QuillHTMLContent;

        public async void GetHTML()
        {
            QuillHTMLContent = await this.QuillHtml.GetHTML();
            StateHasChanged();
        }

        public async void SetHTML()
        {
            string QuillContent =
                @"<a href='http://BlazorHelpWebsite.com/'>" +
                "<img src='images/BlazorHelpWebsite.gif' /></a>";

            await this.QuillHtml.LoadHTMLContent(QuillContent);
            StateHasChanged();
        }
        private async void Publish()
        {
            var blog = await BlogRepository.AddBlog(new Coronassist.Web.Shared.Models.Blog
            {
                Description = await this.QuillHtml.GetHTML(),
                CreatedOn = DateTime.Now,
                Author = "Admin",
                Title = Title
            });
            if(blog != null)
            {

            }
        }
    }
}