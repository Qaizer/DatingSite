using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class ProfileController : AuthorizeController
    {
        UserRepository _userRepository;

        public ProfileController()
        {
            _userRepository = new UserRepository();
        }

        // GET: Profile
        public ActionResult Index(string username)
        {
            if (username != null)
            {
                var user = _userRepository.GetUser(username);
                var profile = user.MapProfileModel();
                return View(profile);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }      
        }

        [HttpPost]
        public ActionResult UpdateUser(ProfileModel profileToUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.UpdateUser(
                            User.Identity.Name,
                            profileToUpdate.Email
                            );
                        //City = profileToUpdate.City om tid finns.
                    }
                catch (Exception e)
                {
                    return View("Error");
                }
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Index", "Profile");
        }

    }
}

