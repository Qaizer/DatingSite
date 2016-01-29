using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MessageRepository
    {
        //Lägger till avsändare, mottagare, text och tidpunkt i Message-tabellen
        public void AddMessage(int sender, int reciever, string text)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                var messageToAdd = new Message
                {
                    Sender = sender,
                    Reciever = reciever,
                    Text = text,
                    Date = DateTime.Now
                };
                context.Message.Add(messageToAdd);
                context.SaveChanges();
            }
        }

        //Returnerar lista över alla meddelanden där angivet användarID är mottagare 
        public IList<Message> GetMessageList(int reciever)
        {
            using (var context = new OnlineDatingDBEntities())
            {
                return context.Message.Where(x => x.Reciever == reciever).ToList();
            }
        }
    }
}
