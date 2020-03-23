using System;
using System.Collections.Generic;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class UserToken
    {
        public DateTime Expiration;

        public string Token { get; set; }
    }
}
