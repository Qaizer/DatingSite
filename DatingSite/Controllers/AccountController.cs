using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(AccountModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new OnlineDatingDBEntities())
                    {
                        var newUserAccount = new UserAccount()
                        {
                            Username = account.Username,
                            Password = account.Password,
                            Email = account.Email,
                            Gender = account.Gender,
                            Birthdate = account.Birthdate,
                        };
                        context.UserAccount.Add(newUserAccount);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.ToString());
                Console.ReadLine();
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var _user = context.UserAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (_user != null)
                {
                    Session["UserID"] = _user.UserAccountID.ToString();
                    Session["UserName"] = _user.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Username or Password");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}