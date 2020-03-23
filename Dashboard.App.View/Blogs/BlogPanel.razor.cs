using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Coronassist.Web.Shared.Models;

namespace Skclusive.Blazor.Dashboard.App.View.Blogs
{
    public partial class BlogPanel
    {
        [Parameter]
        public IEnumerable<Coronassist.Web.Shared.Models.Blog> AllBlogs { get; set; }
    }
}