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
        public IList<Message> GetMessages(string username)
        {
            var reciever = _userRepository.GetUser(username);

            return _messageRepository.GetUserMessageList(reciever.UserAccountID);

        }

        public void AddMessage(int sender, int reciever, string message)
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
