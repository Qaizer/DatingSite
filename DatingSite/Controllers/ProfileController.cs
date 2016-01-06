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
                var user = userRepository.GetUser(username);
                   return View( new ProfileModel
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

        [HttpPost]
        public void UpdateUser(ProfileModel profileToUpdate)
        {
            var userRepository = new UserRepository();
            {
                userRepository.UpdateUser(
                    profileToUpdate.Username,
                    profileToUpdate.Email
                    );
                //City = profileToUpdate.City om tid finns.
            };

        }

    }
}

