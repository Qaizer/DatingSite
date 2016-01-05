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
        public IList<Message> Messages {get; set; }
    }
}