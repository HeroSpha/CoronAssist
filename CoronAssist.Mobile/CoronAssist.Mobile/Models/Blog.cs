using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
