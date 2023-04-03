using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Models.DTOs
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public ICollection<PhotoDTO> Photos { get; set; }
        public ICollection<VideoDTO> Videos { get; set; }
        public string LikeStatus { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public string IsMainComment { get; set; }
        public int LikeCount { get; set; }
        public string AppUser { get; set; }
    }
}