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
        public Photo Photos { get; set; }
        public Videos Videos { get; set; }
        public Comment Comments { get; set; }
        public LikeOrNot LikeStatus { get; set; }
        public AppUser AppUser { get; set; }
    }
}