using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Mvc;
using DataAccessLayer;
using DatingSite.Models;

namespace DatingSite.Controllers.ApiControllers
{
    public class MessageController : ApiController
    {
        private UserRepository _userRepository;
        private MessageRepository _messageRepository;

        public MessageController()
        {
            _userRepository = new UserRepository();
            _messageRepository = new MessageRepository();
        }

        [System.Web.Http.HttpGet]
        public IList<MessageModel> GetMessages()
        {
            var reciever = _userRepository.GetUser(User.Identity.Name);

            var messageReferenceList = _messageRepository.GetMessageList(reciever.UserAccountID);

            return (from m in messageReferenceList
                let sender = _userRepository.GetUser(m.Sender)
                select new MessageModel
                {
                    SenderUsername = sender.Username,
                    Text = m.Text,
                    TimeStamp = DateTime.Now //TODO: Ändra till m.TimeStamp
                    }).ToList();   

        }

        [System.Web.Http.HttpPost]
        public void PostMessage([FromBody]string reciever, [FromBody]string message)
        {
            try
            {
                var sender = User.Identity.Name;
                var senderId = _userRepository.GetUser(sender).UserAccountID;
                var recieverId = _userRepository.GetUser(reciever).UserAccountID;

                _messageRepository.AddMessage(senderId, recieverId, message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
