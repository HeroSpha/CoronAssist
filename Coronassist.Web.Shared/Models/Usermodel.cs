using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coronassist.Web.Shared.Models
{
    public class Usermodel
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Required(ErrorMessage = "Confirm password do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Full name is required")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Valid email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
