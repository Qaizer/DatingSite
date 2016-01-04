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
        private CityRepository cities = new CityRepository();

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

        [Required(ErrorMessage = "Please enter an email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Birthday")]
        public DateTime Birthdate { get; set; }

        public bool Gender { get; set; }

        [Display(Name = "City")]
        public City City { get; set; }

    }
}
