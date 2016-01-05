using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class ProfileController : AuthorizeController
    {
        // GET: Profile
        public ActionResult Index(string username)
        {
            return View(new ProfileModel { Username = username });
        }
    }
}