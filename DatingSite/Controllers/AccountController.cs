using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccessLayer;
using System.Web.Routing;

namespace DatingSite.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string username, string password)
        {
            var userRepository = new UserRepository();
            if (userRepository.Exists(username, password))
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
    }
}