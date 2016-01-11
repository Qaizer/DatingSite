using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FriendshipRepository
    {
        public IList<Friendship> GetFriendships(int userAccountID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var list1 = context.Friendship.Where(x => x.User == userAccountID).ToList();
                var list2 = context.Friendship.Where(x => x.Friend == userAccountID).ToList();

                foreach(var x in list2)
                {
                    list1.Add(x);
                }

                return list1;
            }
        }
    }
}
