using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace DatingSite.Models
{
    public class ProfileModel
    {
        public int UserAccountID { get; set; }
        public string Username { get; set; } 
        public string ImageUrl { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; } 
        public City City { get; set;}
        public string ImagePath { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public string Build { get; set; }
        public string Eyecolor { get; set; }
        public string Haircolor { get; set; }
        public string Origin { get; set; }
        public string CivilStatus { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
        public string Branch { get; set; }

        public bool PendingFriendRequest { get; set; }
        public bool IsFriend { get; set; }
        
        #region selection enums


        #endregion
    }
}