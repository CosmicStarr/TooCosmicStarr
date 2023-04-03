using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Data.Classes
{
    public class VideoService : IVideoService
    {
        private readonly IWebHostEnvironment _web;
        private readonly ILogger<VideoService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly string folder = "videos";
        public VideoService(IWebHostEnvironment web,IHttpContextAccessor httpContext,ILogger<VideoService> logger)
        {
            _httpContext = httpContext;
            _web = web;
            _logger = logger;
        }

        public async Task<string> SaveVideo(IFormFile video)
        {
            try
            {
                //the absolute path to wwwroot
                var webRootPath = _web.WebRootPath;
                //randomizing the file name by creating a guid
                var fileName = Guid.NewGuid().ToString();
                //the path to upload video file coming from angular
                var uploads = Path.Combine(webRootPath,folder);
                //retrieving the extension from the video file
                var extension = Path.GetExtension(video.FileName);
                //opening a filestream to create a videofile to store the uploaded videos 
                if(!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
                using (var fileStream = new FileStream(Path.Combine(uploads,fileName + extension),FileMode.Create))
                {   
                    // using var memoryStream = new MemoryStream();
                    // await video.CopyToAsync(memoryStream);
                    // var vidInfo = memoryStream.ToArray();
                    // await File.WriteAllBytesAsync(fileStream.ToString(),vidInfo);
                    //copying the uploaded video file to the filestream! 
                    await video.CopyToAsync(fileStream);
                }
                // var filePath = @"\videos\" + fileName + extension;
                //Creating an http://localhost.../
                var url = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}";
                return Path.Combine(url,folder,fileName+extension).Replace("\\","/");
                //Create some logic to update a video 
            }
            catch (Exception ex)
            {            
                _logger.LogError(ex, ex.Message);
            }
            return null;
        }
    }
}