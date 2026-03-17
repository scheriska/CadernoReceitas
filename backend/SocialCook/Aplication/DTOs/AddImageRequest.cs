using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialCook.Aplication.DTOs
{
    public class AddImageRequest
    {
        public IFormFile File { get; set; }
        public string? Description { get; set; }
    }
}