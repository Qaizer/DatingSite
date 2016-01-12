using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DatingSite.Controllers
{
    public class FriendshipController : AuthorizeController
    {
        UserRepository _userRepository;
        FriendshipRepository _friendshipRepository;

        public FriendshipController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
        }

        #region Get
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var friendModelList = GetFriendships();
                    var requesterModelList = GetRequests();
                    var model = new FriendshipModel { Friends = friendModelList, FriendRequests = requesterModelList };
                    return View(model);
                }
                catch (Exception e)
                {
                    var model = new ErrorModel { Exception = e };
                    return RedirectToAction("Index", "Error", model);
                }
            }
            return RedirectToAction("Index", "Profile");
        }

        public IList<ProfileModel> GetFriendships()
        {
            List<ProfileModel> friendModelList = new List<ProfileModel>();
            var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
            var friendShipList = _friendshipRepository.GetFriendships(_userID);

            UserAccount friend;
            foreach (var f in friendShipList)
            {
                if (f.User == _userID)
                {
                    friend = _userRepository.GetUser(f.Friend);
                    friendModelList.Add(friend.MapProfileModel());
                }
                else
                {
                    friend = _userRepository.GetUser(f.User);
                    friendModelList.Add(friend.MapProfileModel());
                }
            }
            return friendModelList;
        }

        public IList<ProfileModel> GetRequests()
        {
            var requesterModelList = new List<ProfileModel>();
            var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
            var friendRequestList = _friendshipRepository.GetFriendRequests(_userID);

            UserAccount sender;
            foreach (var f in friendRequestList)
            {
                sender = _userRepository.GetUser(f.Sender);
                requesterModelList.Add(sender.MapProfileModel());
            }
            return requesterModelList;
        }
        #endregion

        #region Set/Update
        public ActionResult SendRequest(string friendUsername)
        {
            var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
            var friendID = _userRepository.GetUser(friendUsername).UserAccountID;
            _friendshipRepository.AddRequest(_userID, friendID);

            return RedirectToAction("Index", "Profile", new { username = friendUsername});
        }

        public ActionResult AcceptRequest(int senderID)
        {
            var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
            _friendshipRepository.AcceptRequest(senderID, _userID);
            return View();
        }
        #endregion
       
    }
}