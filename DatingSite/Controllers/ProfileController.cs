using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DataAccessLayer;
using System.Text;
using DatingSite.Extensions;
using DatingSite.Models;
using System.IO;
using System.Collections;

namespace DatingSite.Controllers
{
    public class ProfileController : AuthorizeController
    {
        UserRepository _userRepository;
        FriendshipRepository _friendshipRepository;

        public ProfileController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
        }

        // GET: Profile
        public ActionResult Index(string username)
        {//Hej4
            if (username != null)
            {
                try {
                    var profileModel = _userRepository.GetUser(username).MapToModel();

                    //Räknar antal obesvarade friendrequests
                    profileModel.RequestCount = _friendshipRepository.RequestCount(profileModel.UserAccountID);

                    //Sätter friendRequest-knappen beroende på om det finns request, vänskap eller inget alls.
                    if (profileModel.Username != User.Identity.Name)
                    {
                        var userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                        var friendID = profileModel.UserAccountID;

                        //Redan requestad
                        if (_friendshipRepository.ExistingRequest(userID, friendID))
                            profileModel.PendingFriendRequest = true;
                        else
                            profileModel.PendingFriendRequest = false;

                        //Redan vänner
                        if (_friendshipRepository.ExistingFriendship(userID, friendID))
                            profileModel.IsFriend = true;
                        else
                            profileModel.IsFriend = false;
                    }
                    return View(profileModel);
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorModel { Exception = e });
                }
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
                    _userRepository.UpdateUser(profileToUpdate.MapToEntity());

                    return RedirectToAction("Index", "Profile");
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorModel { Exception = e });
                }
            }
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        public ActionResult UploadPhoto(HttpPostedFileBase file)
       {
            if (file == null) return RedirectToAction("Index", "Profile");

            try {
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
                return RedirectToAction("Index", "Profile");
            }
            catch(Exception e)
            {
                return View("Error", new ErrorModel { Exception = e });
            }

        }
    }
}

