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
            if (username != null)
            {
                var userRepository = new UserRepository();
                var user = userRepository.getUser(username);
                return View(
                    new ProfileModel
                    {
                        Username = user.Username,
                        Password = user.Password,
                        ImageUrl = user.ImageUrl,
                        Email = user.Email,
                        Build = user.Build,
                        Eyecolor = user.Eyecolor,
                        Haircolor = user.Haircolor,
                        Origin = user.Origin,
                        CivilStatus = user.Civil_Status,
                        Occupation = user.Occupation,
                        Education = user.Education,
                        Branch = user.Branch
                    });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }      
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
        [HttpPost]
        public void UpdatePassword(string name, string oldPassword, string newPassword)
        {
            var userRepository = new UserRepository();

            userRepository.ChangePassword(name, oldPassword, newPassword);
        }

    }
}

