using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models.DTOs
{
    public class GetPostDTO
    {
        public string Photo { get; set; }
        public string Videos { get; set; }
        public string LikeStatus { get; set; }
        public string Comments { get; set; }
        public string AppUser { get; set; }
    }
}