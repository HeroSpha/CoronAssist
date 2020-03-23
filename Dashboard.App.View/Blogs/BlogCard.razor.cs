using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

namespace Skclusive.Blazor.Dashboard.App.View.Blogs
{
    public partial class BlogCard
    {
        [Parameter]
        public Coronassist.Web.Shared.Models.Blog Blog { get; set; }
    }
}