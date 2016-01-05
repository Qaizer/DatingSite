using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace DatingSite.Models
{
    public class ProfileModel
    {
        public string Username { get; set; } 
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; } 
        public string City { get; set;}
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

        public IList<Message> Messages {get; set; }

        #region selection enums


        #endregion
    }
}