using System;
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
            using(var context = new OnlineDatingDBEntities())
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
                try
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
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        #region UpdateUser

        public void ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                UserAccount userToEdit;
                userToEdit = context.UserAccount.FirstOrDefault(x => x.Username == username && x.Password == oldPassword);
                userToEdit.Password = newPassword;
                context.SaveChanges();
            }
        }

        public void ChangeEmail(string user, string newEmail)
        {
            UserAccount userToEdit;
            using (var context = new OnlineDatingDBEntities())
            {
                userToEdit = (from u in context.UserAccount
                              where u.Username == user
                              select u).First();
            }

            if (userToEdit != null)
            {
                userToEdit.Email = newEmail;
            }

            using (var contextModified = new OnlineDatingDBEntities())
            {
                contextModified.Entry(userToEdit).State = EntityState.Modified;
                contextModified.SaveChanges();
            }
        }
        #endregion
    }
}
