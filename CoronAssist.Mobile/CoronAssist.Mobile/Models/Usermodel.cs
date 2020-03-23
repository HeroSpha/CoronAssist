using System;
using System.Collections.Generic;
using System.Text;

namespace CoronAssist.Mobile.Models
{
    public class Usermodel
    {
        
        public string Password { get; set; }
       
        public string ConfirmPassword { get; set; }
    
        public string Fullname { get; set; }
        
        public string Email { get; set; }
    
        public string Role { get; set; }
     
        public string Province { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
