using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace DatingSite.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Please choose a username.")]
        [StringLength(20, ErrorMessage = "Username needs to be between {0} and {2} characters long", MinimumLength = 6)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please choose a password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm password.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter a valid email adress.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [StringLength(30, ErrorMessage = "Email cannot be shorter than {0} or longer than {2}", MinimumLength = 6)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
