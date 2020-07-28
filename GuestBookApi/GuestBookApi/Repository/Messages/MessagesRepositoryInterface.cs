using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using System.Collections.Generic;

namespace GuestBookApi.Repository.Messages
{
    public interface IMessagesRepository
    {
        IEnumerable<MessageView> GetMessages(PagingParameters pagingParameters, out int rowsCount);
        void SaveMessage(Message message);
    }
}