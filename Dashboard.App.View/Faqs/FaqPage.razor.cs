using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.TextEditor;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Faqs
{
    public partial class FaqPage
    {
        [Inject]
        public IFaqRepository FaqRepository { get; set; }
        BlazoredTextEditor QuillHtml;
        string QuillHTMLContent;
        public Faq Faq { get; set; }
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
            var faq = await FaqRepository.AddFaq(new Coronassist.Web.Shared.Models.Faq
            {
                Description = await this.QuillHtml.GetHTML(),
                FaqId = Faq == null ? 0 : Faq.FaqId
            });
            if (faq != null)
            {
                Faq = faq;
            }
        }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                Faq = await FaqRepository.GetFaq();
                if (Faq != null)
                    await QuillHtml.LoadContent(Faq.Description);
                StateHasChanged();
              
            }
            catch (Exception)
            {

            }
        }
    }
}