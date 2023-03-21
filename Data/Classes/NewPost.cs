using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Models;

namespace Data.Classes
{
    public class NewPost : ICreatePost
    {
        private readonly IUnitOfWork _unitOfWork;
        public IPhotoService _photoService; 
        private readonly IVideoService _videoService;
        public NewPost(IUnitOfWork unitOfWork,IPhotoService photoService,IVideoService videoService)
        {
            _videoService = videoService;
            _photoService = photoService;
            _unitOfWork = unitOfWork;
            
        }
        public async Task<Post> CreatePost(PostDTO post, string appUser)
        {            
            var CurrentUser = await _unitOfWork.Repository<AppUser>().GetFirstOrDefault(x =>x.Email == appUser);
            var nuListOfUserPost = new ListOfUserPost();
            nuListOfUserPost.Id = Guid.NewGuid().ToString();
            nuListOfUserPost.CurrentUserPost = new List<Post>();
            var actualNewPost = new Post();

                if(post.file != null)
                {
                    foreach(var item in post.file)
                    {
                        if(item.ContentType == "video/mp4")
                        {
                            var info = await _videoService.SaveVideo(item);
                            actualNewPost.Videos = new Videos
                            {
                                PostVideos = info
                            };
                        }                        
                    }
                    var pics = await _photoService.AddPhotoAsync(post.file);
                    actualNewPost.AppUser = CurrentUser;
                    actualNewPost.Comments = new Comment
                    {
                        ActualComment = post.Comments ?? null
                    };
                    if(pics == null)
                    {
                        actualNewPost.Photos = new Photo()
                        {
                            ApperUserPics = null,
                            PhotoUrl =  null,
                            PublicId = null,
                        };
                    }
                    else
                    {
                        actualNewPost.Photos = new Photo
                        {
                            ApperUserPics = CurrentUser,
                            PhotoUrl = pics.SecureUrl.AbsoluteUri,
                            PublicId = pics.PublicId,
                        };
                    }         
                    actualNewPost.LikeStatus = new LikeOrNot
                    {
                        LikeStatus = "New"
                    }; 
                    nuListOfUserPost.CurrentUserPost.Add(actualNewPost);
                    _unitOfWork.Repository<ListOfUserPost>().Add(nuListOfUserPost);
                    await _unitOfWork.Complete();
                    return actualNewPost;
                }
                actualNewPost.AppUser = CurrentUser;
                actualNewPost.Comments = new Comment
                {
                    ActualComment = post.Comments
                };
                actualNewPost.Photos = new Photo();
                actualNewPost.Videos = new Videos();
                actualNewPost.LikeStatus = new LikeOrNot
                {
                    LikeStatus = "New"
                };
                nuListOfUserPost.CurrentUserPost.Add(actualNewPost);
                _unitOfWork.Repository<ListOfUserPost>().Add(nuListOfUserPost);
                await _unitOfWork.Complete();
                return actualNewPost;
        }
    }
}