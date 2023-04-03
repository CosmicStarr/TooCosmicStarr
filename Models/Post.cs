using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Videos> Videos { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<LikeOrNot> LikeStatus { get; set; }
        public int LikeCount { get; set; }
        public string IsMainComment { get; set; }
        public AppUser AppUser { get; set; }
    }
}