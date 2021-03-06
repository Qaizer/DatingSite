﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class UserRepository
    {
        #region Exist checks
        public bool UserExists(string username, string password)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.Any(x => x.Username == username && x.Password == password);
            }
        }
        
        public bool UsernameExists(string username)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.Any(x => x.Username == username);
            }
        }

        public bool EmailExists(string email)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.Any(x => x.Email == email);
            }
        }
        #endregion

        #region Data manipulation
        public void Add(string username, string password, string email)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var newUser = new UserAccount
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    Searchable = true,
                    Length = "0",
                    Weight = "0",
                    Build = "Unspecified",
                    Eyecolor = "Unspecified",
                    Haircolor = "Unspecified",
                    Origin = "Unspecified",
                    Civil_Status = "Unspecified",
                    Occupation = "Unspecified",
                    Education = "Unspecified",
                    Branch = "Unspecified",
                };
                context.UserAccount.Add(newUser);
                context.SaveChanges();
            }
        }

        public void UpdateUser(string username, string newEmail, bool searchable, string city)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var userToUpdate = context.UserAccount.First(x => x.Username == username);

                userToUpdate.Email = newEmail;
                userToUpdate.Searchable = searchable;
                context.SaveChanges();
            }
        }
        public void UpdateUser(UserAccount user)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var userToUpdate = context.UserAccount.First(x => x.UserAccountID == user.UserAccountID);
                userToUpdate = user;

                context.SaveChanges();
            }
        }
        #endregion

        //Sparar en sökväg till lokalt sparade bilder.
        public void SaveImagePath(string username,string imagePath)
        { 
            using (var context = new OnlineDatingDBEntities())
            {
                var userToUpdate = context.UserAccount.First(x => x.Username == username);
                userToUpdate.ImagePath = imagePath;

                context.SaveChanges();
            }
        }

        #region getData
        public UserAccount GetUser(string username)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.First(x => x.Username == username);
            }
        }
        public UserAccount GetUser(int userAccountID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.First(x => x.UserAccountID == userAccountID);
            }
        }

        //Tar emot en parameter för hur många slumpade användare som efterfrågas och returnerar dessa i en lista.
        public IList<UserAccount> GetRandomUsers(int amount)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                //Listan sorteras efter ett slumpmässigt Guid och sedan returneras utvalt antal som lista.
                return context.UserAccount.OrderBy(o => Guid.NewGuid()).Take(amount).ToList();
            }
        }

        public IList<UserAccount> SearchUser(string searchString)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                //Skapar en lista av alla användare som börjar med en söksträng och har sökbar (serchable) satt till true;
                var searchResultList = context.UserAccount.Where(x => x.Username.StartsWith(searchString) && x.Searchable == true).ToList();

                return searchResultList;
            }
        }

        #endregion
    }
}
