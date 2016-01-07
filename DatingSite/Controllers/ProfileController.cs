using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;
using System.IO;

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

                return View(user.MapProfileModel());
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

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);

                file.SaveAs(path);
            }
            return RedirectToAction("Index", "Profil");
        }
    }
}

