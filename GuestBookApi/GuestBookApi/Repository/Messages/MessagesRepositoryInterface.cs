using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Responce;

namespace GuestBookApi.Repository.Messages
{
    public interface IMessagesRepository
    {
        public MessageListResponce GetMessages(PagingParameters pagingParameters);
        public void SaveMessage(SaveMessageParameters saveMessageParameters, string ip, string browser);
    }
}