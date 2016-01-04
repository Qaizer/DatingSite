using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserRepository
    {
        public bool Exists(string username, string password)
        {
            using(var context = new OnlineDatingDBEntities())
            {
                return context.UserAccount.Any(x => x.Username == username && x.Password == password);
            }
        }

        public void Add(string username, string password)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                try
                {
                    var newUser = new UserAccount
                    {
                        Username = username,
                        Password = password
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
    }
}
