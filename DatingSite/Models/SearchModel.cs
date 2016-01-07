using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccessLayer;

namespace DatingSite.Models
{
    public class SearchModel
    {
        public IList<UserAccount> SearchResult { get; set; }
    }
}