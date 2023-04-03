using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class VideoDTO
    {
        public bool IsMain { get; set; }
        public string PostVideos { get; set; }
        public string AppUserVideos { get; set; }
    }
}