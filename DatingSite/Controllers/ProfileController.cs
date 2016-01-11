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
                    return RedirectToAction("Index", "Error", new ErrorModel { Exception = e});
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
                var user = User.Identity.Name;
                var oldPic = _userRepository.GetUser(user).ImagePath;

                if (oldPic != null)
                {
                    System.IO.File.Delete(oldPic);
                }

                var pic = Path.GetFileName(file.FileName);
                var path = Path.Combine(
                                       Server.MapPath("~/Content/ProfilePictures"), pic);

                file.SaveAs(path);
                _userRepository.SaveImagePath(user, pic);
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}

