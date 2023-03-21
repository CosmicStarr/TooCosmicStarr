using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Data.Classes
{
    public class PhotoService :IPhotoService
    {
        private readonly Cloudinary _cloud;
        public PhotoService(IOptions<CloudinaryPhotoInfo> config)
        {
            var CloudAccount = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloud = new Cloudinary(CloudAccount);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(ICollection<IFormFile> file)
        {
            foreach(var item in file)
            {
                if(item.ContentType != "video/mp4")
                {
                    var UploadedImage = new ImageUploadResult();
                    if(item.Length > 0)
                    {
                        using var stream = item.OpenReadStream();
                        var UploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(item.FileName,stream),
                            Transformation = new Transformation().Height(612).Width(612).Crop("fill").Gravity("face")
                        };
                        UploadedImage = await _cloud.UploadAsync(UploadParams);
                    }
                    return UploadedImage;
                }
            }
            return null;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var deletePic = new DeletionParams(PublicId);
            var results = await _cloud.DestroyAsync(deletePic);
            return results;
        }
    }
}