using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DatingSite.Models;

namespace DatingSite.Extensions
{
    public static class EntityExtensions
    {
        public static ProfileModel MapProfileModel(this UserAccount user)
        {


            return new ProfileModel
            {
                Username = user.Username,
                Password = user.Password,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                Build = user.Build,
                Eyecolor = user.Eyecolor,
                Haircolor = user.Haircolor,
                Origin = user.Origin,
                CivilStatus = user.Civil_Status,
                Occupation = user.Occupation,
                Education = user.Education,
                Branch = user.Branch
            };
        }
    }
}