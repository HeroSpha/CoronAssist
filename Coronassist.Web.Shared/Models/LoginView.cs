using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class LoginView
    {
        public LoginView()
        {
            Errors = new HashSet<string>();
        }
        public virtual ICollection<string> Errors { get; set; }
        public string Role { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
