
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class ApplicationUser 
    {
        public ApplicationUser()
        {
            Books = new HashSet<Book>();
            AccountSurveys = new HashSet<AccountSurvey>();
        }
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<AccountSurvey> AccountSurveys { get; set; }
    }
}
