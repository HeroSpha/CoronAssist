using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Blogs
{
    public partial class BlogPage
    {
        [Inject]
        public IBlogRepository BlogRepository { get; set; }
        public IEnumerable<Coronassist.Web.Shared.Models.Blog> BlogList { get; set; }
        public BlogPage()
        {
            BlogList = new HashSet<Coronassist.Web.Shared.Models.Blog>();
        }
        protected async override Task OnInitializedAsync()
        {
            BlogList = await BlogRepository.GetBlogs();
           
        }
    }
}