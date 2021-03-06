﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DatingSite.Models;

namespace DatingSite.Controllers
{
    public class SearchController : AuthorizeController
    {
        readonly UserRepository _userRepository;

        public SearchController()
        {
            _userRepository = new UserRepository();
       
        }
        // GET: Search

        public ActionResult SearchDisplay(string searchString)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = new SearchModel() { SearchResult = _userRepository.SearchUser(searchString) };

                    return View(result);
                }
                catch(Exception e)
                {
                    return View("Error", new ErrorModel { Exception = e });
                }
            }
            return View();
        }
    }
}