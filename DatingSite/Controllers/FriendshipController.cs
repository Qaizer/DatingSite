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
                    return View("Error", new ErrorModel { Exception = e });
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
                //Hämtar inloggad användares ID
                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                //Hämtar alla vänskaper användaren är en del av.
                var friendShipList = _friendshipRepository.GetFriendships(_userID);

                //En användare kan vara både mottagare och sändare av en vänskap, och kan därför
                //förekommer i två olika kolumner.
                //Här mappas alla användarens vänner om till ProfilModeller.
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

        public ActionResult DeleteFriendship(int friendId)
        {
            try
            {
                var userId = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                _friendshipRepository.DeleteFriendship(userId, friendId);
                
            }
            catch(Exception e)
            {
                return View("Error", new ErrorModel { Exception = e});
            }
            return RedirectToAction("Index");
        }

        public IList<ProfileModel> GetRequests()
        {
            try
            {
                var requesterModelList = new List<ProfileModel>();
                //Hämtar inloggad användares ID
                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                //Hämtar alla vänskapsförfrågningar användaren fått.
                var friendRequestList = _friendshipRepository.GetFriendRequests(_userID);

                //Mappar om alla förfrågningars avsändare till ProfileModels
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
                //Hämtar Idn för avsändare och mottagare via användarnamn och ber repository lägga till dessa i
                //sambandstabllen FriendRequest.
                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                var friendID = _userRepository.GetUser(friendUsername).UserAccountID;
                _friendshipRepository.AddRequest(_userID, friendID);
                return RedirectToAction("Index", "Profile", new { username = friendUsername });
            }
            catch (Exception e)
            {
                return View("Error", new ErrorModel { Exception = e });
            }
        }

        public ActionResult AcceptRequest(int senderID)
        { 
            try
            {
                var user = Session["User"] as ProfileModel;

                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                //Repositoryt tar bort requesten och skapar en friendship.
                _friendshipRepository.AcceptRequest(senderID, _userID);

                //Uppdaterar antalet obesvarade requests
                user.RequestCount = _friendshipRepository.RequestCount(user.UserAccountID);
                Session["User"] = user;

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorModel { Exception = e });
            }
        }

        public ActionResult DeclineRequest(int senderID)
        {
            try
            {
                var user = Session["User"] as ProfileModel;

                var _userID = _userRepository.GetUser(User.Identity.Name).UserAccountID;
                //Repositoryt tar bort requesten.
                _friendshipRepository.DeleteRequest(senderID, _userID);

                //Uppdaterar antalet obesvarade requests
                user.RequestCount = _friendshipRepository.RequestCount(user.UserAccountID);
                Session["User"] = user;

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", new ErrorModel { Exception = e });
            }
        }
        #endregion  
    }
}