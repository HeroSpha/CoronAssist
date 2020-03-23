using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class UserToken
    {
        public DateTime Expiration;

        public string Token { get; set; }
    }
}
