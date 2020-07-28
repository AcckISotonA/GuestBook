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
using System.Linq;

namespace GuestBookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageListService _messageListService;

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
                return Ok(_messageListService.GetMessageList(pagingParameters));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("savemessage")]
        public IActionResult SaveMessage([FromBody]MessageRequest messageRequest)
        {
            try
            {
                var userAgent = HttpContext.Request.Headers.ContainsKey("User-Agent") ? HttpContext.Request.Headers["User-Agent"].FirstOrDefault() : null;

                _messageListService.SaveMessage(messageRequest, HttpContext.Connection.RemoteIpAddress.ToString(), userAgent);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new StatusCodeResult( 500 );
            }
        }
    }
}
