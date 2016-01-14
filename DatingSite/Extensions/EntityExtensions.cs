using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DatingSite.Models;
using DatingSite.Controllers.ApiControllers;

namespace DatingSite.Extensions
{
    public static class EntityExtensions
    {
        public static ProfileModel MapProfileModel(this UserAccount user)
        {
            return new ProfileModel
            {
                UserAccountID = user.UserAccountID,
                Username = user.Username,
                Password = user.Password,
                ImagePath = user.ImagePath,
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