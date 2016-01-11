using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatingSite.Models
{
    public class FriendsModel
    {
        public IList<ProfileModel> Friends { get; set; }
    }
}