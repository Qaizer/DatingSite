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
    }
}
