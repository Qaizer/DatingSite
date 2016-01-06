using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using System.Web.Routing;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class AccountController : Controller
    {
        UserRepository _userRepository;

        public AccountController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Login(string username, string password)
        {
            if (_userRepository.UserExists(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return RedirectToAction("Index", "Profile", new RouteValueDictionary(new { username }));
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
        public ActionResult Register(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                if(_userRepository.UsernameExists(account.Username))
                {
                    return View(); //Ska visa ett error vid fältet
                }
                else if(_userRepository.EmailExists(account.Email)){
                    return View(); //Ska visa ett error vid fältet
                }
                else
                {
                    _userRepository.Add(account.Username, account.Password, account.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                System.Console.WriteLine("Register modelstate is not valid.");
                return View();
            }
        }
    }
}