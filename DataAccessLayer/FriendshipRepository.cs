using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FriendshipRepository
    {
        #region Get
        public IList<Friendship> GetFriendships(int userAccountID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var list = context.Friendship.Where(x => x.User == userAccountID).ToList();
                var list2 = context.Friendship.Where(x => x.Friend == userAccountID).ToList();

                foreach(var x in list2)
                {
                    list.Add(x);
                }

                return list;
            }
        }

        public IList<FriendRequest> GetFriendRequests(int userAccountID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.FriendRequest.Where(x => x.Reciever == userAccountID).ToList();
            }
        }
        #endregion

        #region Exists
        public bool ExistingRequest(int sender, int reciever)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.FriendRequest.Any
                (
                    x => x.Sender == sender && x.Reciever == reciever 
                    || x.Sender == reciever && x.Reciever == sender

                );
            }
                
        }

        public bool ExistingFriendship(int sender, int reciever)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.Friendship.Any
                (
                    x => x.User == sender && x.Friend == reciever
                    || x.User == reciever && x.Friend == sender
                );
            }

        }
        #endregion

        #region Add
        public void AddRequest(int sender, int reciever)
        {
            using(var context = new OnlineDatingDBEntities())
            {
                var newRequest = new FriendRequest
                {
                    Sender = sender,
                    Reciever = reciever
                };
                context.FriendRequest.Add(newRequest);
                context.SaveChanges();
            }
        }

        public void AcceptRequest(int user1, int user2)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var newFriendship = new Friendship
                {
                    User = user1,
                    Friend = user2,
                    StartDate = DateTime.Now
                };

                var request = context.FriendRequest.First
                (
                    x => x.Sender == user1 && x.Reciever == user2
                );

                context.Friendship.Add(newFriendship);
                context.FriendRequest.Remove(request);
                context.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteFriendship(int user, int friend)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var friendship = context.Friendship.First
                    (
                        x => x.User == user && x.Friend == friend
                        || x.User == friend && x.Friend == user
                    );
                context.Friendship.Remove(friendship);
                context.SaveChanges();
            }
        }

        public void DeleteRequest(int sender, int reciever)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var request = context.FriendRequest.First
                    (
                        x => x.Sender == sender && x.Reciever == reciever
                        || x.Sender == reciever && x.Reciever == sender
                    );
                context.FriendRequest.Remove(request);
                context.SaveChanges();
            }
        }

        #endregion

    }
}
