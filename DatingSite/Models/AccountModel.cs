using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer;
using DatingSite.Resources;

namespace DatingSite.Models
{
    public class AccountModel
    {
        //Username
        [Required(
            ErrorMessageResourceName = "UsernameReqError", 
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [StringLength(20,
            ErrorMessageResourceName = "UsernameFormatError",
            ErrorMessageResourceType = typeof(RegisterStrings),
            MinimumLength = 3)]
        [Display(Name = "Username", ResourceType = typeof(RegisterStrings))]
        public string Username { get; set; }    

        //Password
        [Required(
            ErrorMessageResourceName = "PasswordReqError",
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(RegisterStrings))]
        public string Password { get; set; }

        //Confirm Password
        [Required(
            ErrorMessageResourceName = "ConfirmPasswordReqError",
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [DataType(DataType.Password)]
        [Compare("Password",
            ErrorMessageResourceName = "ConfirmPasswordMatchError",
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(RegisterStrings))]
        public string ConfirmPassword { get; set; }

        //Email address
        [Required(
            ErrorMessageResourceName = "EmailReqError",
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(
            ErrorMessageResourceName = "EmailFormatError",
            ErrorMessageResourceType = typeof(RegisterStrings))]
        [Display(Name = "Email", ResourceType = typeof(RegisterStrings))]
        public string Email { get; set; }
    }
}
