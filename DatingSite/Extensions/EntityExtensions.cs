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
        public static ProfileModel MapToModel(this UserAccount user)
        {

            //Nytt objekt av Profilemodel med NOT NULL data.
            var profileModel = new ProfileModel
            {
                UserAccountID = user.UserAccountID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Length = user.Length,
                Weight = user.Weight,
                Build = user.Build,
                Eyecolor = user.Eyecolor,
                Haircolor = user.Haircolor,
                Origin = user.Origin,
                CivilStatus = user.Civil_Status,
                Occupation = user.Occupation,
                Education = user.Education,
                Branch = user.Branch
            };
        return profileModel;
        }

        public static UserAccount MapToEntity(this ProfileModel user)
        {

            //Nytt objekt av Profilemodel med NOT NULL data.
            var userAccount = new UserAccount
            {
                UserAccountID = user.UserAccountID,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Length = user.Length,
                Weight = user.Weight,
                Build = user.Build,
                Eyecolor = user.Eyecolor,
                Haircolor = user.Haircolor,
                Origin = user.Origin,
                Civil_Status = user.CivilStatus,
                Occupation = user.Occupation,
                Education = user.Education,
                Branch = user.Branch
            };
            return userAccount;
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