using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Response;
using GuestBookApi.Repository.Messages;
using Microsoft.Extensions.Configuration;

namespace GuestBookApi.Services.MessageList
{
    public class MessageListService : IMessageListService
    {
        private IMessagesRepository _messagesRepository;

        public MessageListService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public MessageListResponse GetMessageList(PagingParameters pagingParameters)
        {
            int rowsCount = 0;
            MessageListResponse result = new MessageListResponse
            {
                MessageList = _messagesRepository.GetMessages(pagingParameters, out rowsCount),
                RowsCount = rowsCount
            };
            return result;
        }

        public void SaveMessage(MessageRequest messageRequest, string ip, string browser)
        {
            var message = new Message {
                UserName = messageRequest.UserName,
                Email = messageRequest.Email,
                Homepage = messageRequest.Homepage,
                Text = messageRequest.Text,
                Ip = ip,
                Browser = browser
            };

            _messagesRepository.SaveMessage(message);
        }
    }
}
