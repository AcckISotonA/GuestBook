using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApi.Models.MessageList
{
    public class Message
    {
        public DateTime DateC { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}
