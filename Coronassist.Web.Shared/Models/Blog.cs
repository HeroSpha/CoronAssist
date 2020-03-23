using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
