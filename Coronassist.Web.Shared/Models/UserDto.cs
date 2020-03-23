using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class UserDto
    {
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Valid email is required")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
