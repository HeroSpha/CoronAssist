using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Books = new HashSet<Book>();
            AccountSurveys = new HashSet<AccountSurvey>();
        }
        public string Fullname { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<AccountSurvey> AccountSurveys { get; set; }
    }
}
