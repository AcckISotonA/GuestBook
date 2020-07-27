using GuestBookApi.Models.MessageList;
using System;
using System.Collections.Generic;
using System.Data;

namespace GuestBookApi.Models.Responce
{
    public class MessageListResponce
    {
        public List<Message> MessageList { get; set; } = new List<Message>();
        public int RowsCount { get; set; }
    }
}
