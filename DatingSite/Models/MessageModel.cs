using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace DatingSite.Models
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public string RecieverUsername { get; set; }
        public string SenderUsername { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}