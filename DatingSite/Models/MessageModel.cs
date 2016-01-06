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
        public UserAccount Sender { get; set; }
        public UserAccount Reciever { get; set; }

        [Required(ErrorMessage = "Please enter a message.")]
        public string Message { get; set; }

        public DateTime DateTime { get; set; }
    }
}