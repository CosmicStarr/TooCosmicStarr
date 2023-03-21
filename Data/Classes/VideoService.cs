using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Data.Classes
{
    public class VideoService : IVideoService
    {

        public async Task<byte[]> SaveVideo(IFormFile video)
        {
            using (var file = video.OpenReadStream())
            {
                using var msStream = new MemoryStream();
                
                    await file.CopyToAsync(msStream);
                    var buffer = new byte[msStream.Length];
                    return msStream.ToArray();
            }
 
        }
    }
}