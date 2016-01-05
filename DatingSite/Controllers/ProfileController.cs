using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class ProfileController : AuthorizeController
    {
        // GET: Profile
        public ActionResult Index(string username)
        {
            return View(new ProfileModel { Username = username });
        }

        public void UpdateEmail(string username, string email)
        {
            var userRepository = new UserRepository();

            if (!userRepository.EmailExists(email))
            {
                userRepository.ChangeEmail(username, email);
            }
            else
            {
                //error message
            }
        }

        public void UpdatePassword(string username, string oldPassword, string newPassword)
        {
            var userRepository = new UserRepository();

            userRepository.ChangePassword(username, oldPassword, newPassword);
        }

    }
}

