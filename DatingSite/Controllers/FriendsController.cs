using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public class FriendsController : Controller
    {
        UserRepository _userRepository;
        FriendshipRepository _FriendshipRepository;

        // GET: Friends
        public ActionResult Index()
        {
            var user = User.Identity.Name;
            return View();
        }
    }
}