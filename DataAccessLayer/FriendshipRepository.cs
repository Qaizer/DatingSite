using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FriendshipRepository
    {
        public IList<Friendship> GetFriends(int userAccountID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.Friendship.Where(x => x.User == userAccountID).ToList();
            }
        }
    }
}
