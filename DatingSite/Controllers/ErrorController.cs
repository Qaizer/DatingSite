using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public class ErrorController : AuthorizeController
    {
        // GET: Error
        public ActionResult Index(ErrorModel model)
        {
            return View(model);
        }
    }
}