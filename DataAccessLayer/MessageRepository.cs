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
            }
        }
    }
}
