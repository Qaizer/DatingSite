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
                    Email = email
                };
                context.UserAccount.Add(newUser);
                context.SaveChanges();
            }
        }

        public void UpdateUser(string username, string emailToUpdate)
        {

            using (var context = new OnlineDatingDBEntities())
            {
                var userToUpdate = GetUser(username);
                userToUpdate.Email = emailToUpdate;

                context.SaveChanges();
            }
        }
        #endregion

        #region getData
        public UserAccount GetUser(string username)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.First(x => x.Username == username);
            }
        } 

        #endregion
    }
}
