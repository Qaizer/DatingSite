using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;


namespace DatingSite.Controllers
{
    public class HomeController : Controller
    {
        readonly UserRepository _userRepository;

        public HomeController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Index()
        {
            if (!ModelState.IsValid) return View();
            var amount = User.Identity.IsAuthenticated ? 5 : 4;
            try
            {
                var userList = _userRepository.GetRandomUsers(amount);
                IList<ProfileModel> profileModels = userList.Select(userAccount => userAccount.MapProfileModel()).ToList();
                return View(profileModels);
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "What are you doing? Go out, see the sun and meet real people. Laugh, Love, Live! <3";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}