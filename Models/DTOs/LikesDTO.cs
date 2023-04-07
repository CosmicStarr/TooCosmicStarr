using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LikesDTO
    {
        public bool Like { get; set; }
        public string LikeStatusDTO { get; set; }
        public string AppUserLikes { get; set; }
    }
}