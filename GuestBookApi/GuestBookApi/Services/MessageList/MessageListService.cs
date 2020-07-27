using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Responce;
using GuestBookApi.Repository.Messages;
using Microsoft.Extensions.Configuration;

namespace GuestBookApi.Services.MessageList
{
    public class MessageListService : IMessageListService
    {
        private IMessagesRepository _messagesRepository;

        public MessageListService(IConfiguration configuration)
        {
            _messagesRepository = new MessagesRepository(configuration);
        }

        public MessageListResponce GetMessageList(PagingParameters pagingParameters)
        {
            return _messagesRepository.GetMessages(pagingParameters);
        }

        public void SaveMessage(SaveMessageParameters saveMessageParameters, string ip, string browser)
        {
            _messagesRepository.SaveMessage(saveMessageParameters, ip, browser);
        }
    }
}
