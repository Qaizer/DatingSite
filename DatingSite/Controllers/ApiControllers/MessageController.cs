using DataAccessLayer;
using DatingSite.Extensions;
using DatingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

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
        public JsonResult<string> PostMessage(MessageModel message)
        {
            try
            {
                var senderId = _userRepository.GetUser(message.SenderUsername).UserAccountID;
                var recieverId = _userRepository.GetUser(message.RecieverUsername).UserAccountID;

                if (message.Text != null && message.Text.Length < 250)
                {
                    _messageRepository.AddMessage(senderId, recieverId, message.Text);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }
            return Json("'Success':'true'");
        }
    }
}
