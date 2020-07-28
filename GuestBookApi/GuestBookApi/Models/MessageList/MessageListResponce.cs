using GuestBookApi.Models.MessageList;
using System.Collections.Generic;

namespace GuestBookApi.Models.Response
{
    public class MessageListResponse
    {
        public IEnumerable<MessageView> MessageList { get; set; }
        public int RowsCount { get; set; }
    }
}
