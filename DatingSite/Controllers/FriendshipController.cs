using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatingSite.Controllers
{
    public class FriendshipController : Controller
    {
        UserRepository _userRepository;
        FriendshipRepository _friendshipRepository;

        public FriendshipController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
        }

        // GET: Friendships
        public ActionResult Index()
        {
            var friendModelList = new List<ProfileModel>();
            var userAccount = _userRepository.GetUser(User.Identity.Name);

            var friendShipList = _friendshipRepository.GetFriendships(userAccount.UserAccountID);
            
            foreach(var f in friendShipList)
            {
                if (f.User == userAccount.UserAccountID)
                {
                    var friend = _userRepository.GetUser(f.Friend);
                    friendModelList.Add(friend.MapProfileModel());
                }
                else
                {
                    var friend = _userRepository.GetUser(f.User);
                    friendModelList.Add(friend.MapProfileModel());
                }
            }

            var model = new FriendsModel { Friends = friendModelList };
                        
            return View(model);
        }
    }
}