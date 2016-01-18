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
    //TODO: Rewrite this class and FriendshipRepository so that all methods take string username and fetch ID in same context.
    public class FriendshipController : AuthorizeController
    {
        UserRepository _userRepository;
        FriendshipRepository _friendshipRepository;

        public FriendshipController()
        {
            _userRepository = new UserRepository();
            _friendshipRepository = new FriendshipRepository();
        }

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
                    return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
                }
            }
            return RedirectToAction("Index", "Profile");
        }

        #region Get
        public IList<ProfileModel> GetFriendships()
        {
            try
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ProfileModel>();
            }
        }

        public IList<ProfileModel> GetRequests()
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ProfileModel>();
            }
        }

        #endregion

        #region Set/Update
        public ActionResult SendRequest(string friendUsername)
        {
            try
            {
                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                var friendID = _userRepository.GetUser(friendUsername).UserAccountID;
                _friendshipRepository.AddRequest(_userID, friendID);
                return RedirectToAction("Index", "Profile", new { username = friendUsername });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
            }
        }

        public ActionResult AcceptRequest(int senderID)
        { 
            try
            {
                var user = Session["User"] as ProfileModel;

                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                _friendshipRepository.AcceptRequest(senderID, _userID);

                user.RequestCount = _friendshipRepository.RequestCount(user.UserAccountID);
                Session["User"] = user;

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new ErrorModel { Exception = e });
            }
        }
        #endregion  
    }
}