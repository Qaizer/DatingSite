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

namespace DatingSite.Controllers
{
    public class ProfileController : AuthorizeController
    {
        UserRepository _userRepository;
        FriendshipRepository _friendshipRepository;
        CityRepository _cityRepository;

        public ProfileController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
            _cityRepository = new CityRepository();
        }

        // GET: Profile
        public ActionResult Index(string username)
        {
            if (username != null)
            {
                try {
                    var userAccount = _userRepository.GetUser(username);
                    var profileModel = userAccount.MapProfileModel();
                    profileModel.RequestCount = _friendshipRepository.RequestCount(profileModel.UserAccountID);

                    var cities = _cityRepository.GetAll();
                    profileModel.CityList = cities.Select(x => x.MapCityModel()).ToList();

                    if (profileModel.Username != User.Identity.Name)
                    {
                        var userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                        var friendID = profileModel.UserAccountID;

                        if (_friendshipRepository.ExistingRequest(userID, friendID))
                            profileModel.PendingFriendRequest = true;
                        else
                            profileModel.PendingFriendRequest = false;

                        if (_friendshipRepository.ExistingFriendship(userID, friendID))
                            profileModel.IsFriend = true;
                        else
                            profileModel.IsFriend = false;

                    }
                    return View(profileModel);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
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
                    _userRepository.UpdateUser(
                            User.Identity.Name,
                            profileToUpdate.Email,
                            profileToUpdate.Searchable
                            //City = profileToUpdate.City om tid finns.
                            );

                    return RedirectToAction("Index", "Profile");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Error", new ErrorModel { Exception = e});
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
                return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
            }
        }
    }
}

