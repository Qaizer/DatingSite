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
            var profileModel = new ProfileModel
            {
                UserAccountID = user.UserAccountID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
            };

            profileModel.ImagePath = user.ImagePath;
            profileModel.Build = (user.Build == null) ? "Unspecified" : user.Build;
            profileModel.Eyecolor = (user.Eyecolor == null) ? "Unspecified" : user.Eyecolor;
            profileModel.Haircolor = (user.Haircolor == null) ? "Unspecified" : user.Haircolor;
            profileModel.Origin = (user.Origin == null) ? "Unspecified" : user.Origin;
            profileModel.CivilStatus = (user.Civil_Status == null) ? "Unspecified" : user.Civil_Status;
            profileModel.Occupation = (user.Occupation == null) ? "Unspecified" : user.Occupation;
            profileModel.Education = (user.Education == null) ? "Unspecified" : user.Education;
            profileModel.Branch = (user.Branch == null) ? "Unspecified" : user.Branch;

            return profileModel; 
        }

        public static CityModel MapCityModel(this City city)
        {
            return new CityModel
            {
                CityID = city.CityID,
                Name = city.Name
            };
        }
    }
}