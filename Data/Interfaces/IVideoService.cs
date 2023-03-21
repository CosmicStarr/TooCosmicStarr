using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Data.Interfaces
{
    public interface IVideoService
    {
        Task<byte[]> SaveVideo(IFormFile video);
    }
}