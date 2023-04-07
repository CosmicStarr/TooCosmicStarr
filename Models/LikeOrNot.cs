using System.Runtime.Serialization;

namespace Models
{
    public class LikeOrNot
    {
        public int Id { get; set; }
        public bool Likes { get; set; }
        public string LikeStatus { get; set; }
        public int LikeCount { get; set; }
        public AppUser AppUserLikes { get; set; }
    }
}