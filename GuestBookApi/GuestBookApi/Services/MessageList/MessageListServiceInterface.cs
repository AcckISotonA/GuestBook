using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApi.Services.MessageList
{
    public interface IMessageListService
    {
        MessageListResponce GetMessageList(PagingParameters pagingParameters);
        void SaveMessage(SaveMessageParameters saveMessageParameters, string ip, string browser);
    }
}
