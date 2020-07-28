using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApi.Services.MessageList
{
    public interface IMessageListService
    {
        MessageListResponse GetMessageList(PagingParameters pagingParameters);
        void SaveMessage(MessageRequest messageRequest, string ip, string browser);
    }
}
