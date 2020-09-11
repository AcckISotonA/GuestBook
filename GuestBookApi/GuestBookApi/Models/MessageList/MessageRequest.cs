using GuestBookApi.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBookApi.Models.MessageList
{
    public class MessageRequest
    {
        [Required(ErrorMessage = "User Name обязателен для заполнения")]
        [UserNameValidation]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [RegularExpression(@"^[a-z0-9_.+-]+@[a-z0-9-]+\.[a-z0-9-.]+$", ErrorMessage = "Email не соответствует формату электронной почты")]
        public string Email { get; set; }

        public string Homepage { get; set; }

        [Required(ErrorMessage = "Text обязателен для заполнения")]
        public string Text { get; set; }
    }
}
