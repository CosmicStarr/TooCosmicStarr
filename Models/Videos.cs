using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Videos
    {
        public int Id { get; set; }
        public bool IsMain { get; set; }
        public string PostVideos { get; set; }
        public AppUser AppUserVideos { get; set; }
    }
}