using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    //Controllers som ärver denna kräver authorisering för att kunna anropas.
    [Authorize]
    public class AuthorizeController : Controller
    {
    }
}