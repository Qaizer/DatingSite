using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        [HttpGet]
        public IList<MessageModel> GetMessages()
        {
            var messages = new List<MessageModel>();
            var reciever = _userRepository.GetUser(User.Identity.Name);

            var messageReferenceList = _messageRepository.GetMessageList(reciever.UserAccountID);

            foreach(var m in messageReferenceList)
            {
                var sender = _userRepository.GetUser(m.Sender);
                var model = new MessageModel
                {
                    SenderUsername = sender.Username,
                    Text = m.Text,
                    TimeStamp = DateTime.Now //TODO: Ändra till m.TimeStamp
                };
                messages.Add(model);
            }
            return messages;        

        }

        [HttpPost]
        public void PostMessage(MessageModel model)
        {
            try
            {
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
