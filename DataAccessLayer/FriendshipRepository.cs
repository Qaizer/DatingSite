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
        //Returnerar antal obesvarade friendRequests för ett angivet användarID
        public int RequestCount(int userID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.FriendRequest.Where(x => x.Reciever == userID).Count();
            }
        }

        //Returnerar lista över alla Friendships för ett angivet användarID
        public IList<Friendship> GetFriendships(int userID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var list = context.Friendship.Where(x => x.User == userID).ToList(); //Där userID finns i kolumnen User
                var list2 = context.Friendship.Where(x => x.Friend == userID).ToList(); //Där userID finns i kolumnen Friend

                //Listorna slås ihop
                foreach (var x in list2)
                {
                    list.Add(x);
                }

                return list;
            }
        }

        //Returnerar en lista över alla obesvarade FriendRequests för ett anvigvet användarID
        public IList<FriendRequest> GetFriendRequests(int userID)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.FriendRequest.Where(x => x.Reciever == userID).ToList();
            }
        }
        #endregion

        #region Exists
        //Returnerar true om två användare redan har en obesvarad request sinsemellan oavsett vem som skickade den.
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
        //Returnerar true om två användare redan har en obesvarad request sinsemellan oavsett vem som skickade requesten.
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
        //Lägger till avsändare och mottagare mellan angivna användare i FriendRequest-tabellen
        public void AddRequest(int sender, int reciever)
        {
            using(var context = new OnlineDatingDBEntities())
            {
                //Kastar exception om Friendship eller FriendRequest redan finns i någon kombination av sender och reciever     
                if (context.Friendship.Any(x => x.User == sender && x.Friend == reciever || x.User == reciever && x.Friend == sender
                || context.FriendRequest.Any(y => y.Sender == sender && y.Reciever == reciever || y.Sender == reciever && y.Reciever == sender)))
                {
                    throw new Exception("Already requested or friends");
                }
                else
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
        }

        /*Lägger till relation mellan angivna användare i Friendship-tabellen.
        Det UserAccountID som var Sender på FriendRequest blir User i Friendship, 
        och det UserAccountID som var Reciever av FriendRequest blir Friend i Friendship
        */
        public void AcceptRequest(int user1, int user2)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                //Kastar exception om Friendship redan finns i någon kombination av sender och reciever
                if (context.Friendship.Any(x => x.User == user1 && x.Friend == user2 || x.User == user2 && x.Friend == user1))
                {
                    throw new Exception("Already friends");
                }
                else
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
                    context.SaveChanges(); ;
                }
            }
        }
        #endregion

        #region Delete

        //Tar bort två angedda användarIDn ur Friendship-tabellen
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

        //Tar bort två angedda användarIDn ur FriendRequest-tabellen
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
