﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(string username, string password, string email)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var newUser = new UserAccount
                {
                    Username = username,
                    Password = password,
                    Email = email
                };
                context.UserAccount.Add(newUser);
                context.SaveChanges();
            }
        }
    }
}
