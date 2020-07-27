using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Threading.Tasks;
using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Services.MessageList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GuestBookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private IMessageListService _messageListService;

        public MessagesController(ILogger<MessagesController> logger, IMessageListService messageListService)
        {
            _logger = logger;
            _messageListService = messageListService;
        }

        [HttpGet("getmessages")]
        public IActionResult GetMessages([FromQuery]PagingParameters pagingParameters)
        {
            try
            {

                return new JsonResult(_messageListService.GetMessageList(pagingParameters));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        [HttpPut("savemessage")]
        public IActionResult SaveMessage([FromBody]SaveMessageParameters saveMessageParameters)
        {
            try
            {
                var userAgent = HttpContext.Request.Headers["User-Agent"];

                _messageListService.SaveMessage(saveMessageParameters, HttpContext.Connection.RemoteIpAddress.ToString(), Convert.ToString(userAgent[0]));
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
