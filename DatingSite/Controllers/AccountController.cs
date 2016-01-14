﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using System.Web.Routing;
using DatingSite.Models;
using DatingSite.Extensions;

namespace DatingSite.Controllers
{
    public class AccountController : Controller
    {
        readonly UserRepository _userRepository;
        readonly FriendshipRepository _friendshipRepository;

        public AccountController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
        }

        public ActionResult Login(string username, string password)
        {
            if (_userRepository.UserExists(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);

                var user = _userRepository.GetUser(username).MapProfileModel();
                user.RequestCount = _friendshipRepository.RequestCount(user.UserAccountID);

                Session["User"] = user;

                return RedirectToAction("Index", "Profile", new RouteValueDictionary(new { username = user.Username }));
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountModel model)
        {
            try
            {
                if (_userRepository.UsernameExists(model.Username))
                {
                    ModelState.AddModelError("Username", "This username already exists, please try another.");
                }
                if (_userRepository.EmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "This email already exists, please try another.");
                }

                if (ModelState.IsValid)
                {
                    _userRepository.Add(model.Username, model.Password, model.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Error", e);
            }
            return View(model);
        }
    }
}