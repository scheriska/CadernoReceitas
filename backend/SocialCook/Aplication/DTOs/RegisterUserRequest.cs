using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialCook.Domain.Emun;

namespace SocialCook.Aplication.DTOs
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set;}
        public string Password { get; set; }
        public ProfileType ProfileType { get; set; }
    }
}