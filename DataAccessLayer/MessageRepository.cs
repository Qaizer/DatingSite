using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MessageRepository
    {
        public void AddMessage(int sender, int reciever, string message, DateTime date)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var messageToAdd = new Message
                {
                    Sender = sender,
                    Reciever = reciever,
                    Message1 = message,
                    Date = date
                };
                context.Message.Add(messageToAdd);
                context.SaveChanges();
            }
        }

        public IList<Message> GetUserMessageList(int reciever)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.Message.Where(x => x.Reciever == reciever).ToList();
            }
        }
    }
}
