using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApi.Models.MessageList
{
    public class Message
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Homepage { get; set; }
        public string Text { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
    }
}
