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
            //Get the current user 
            var CurrentUser = await _unitOfWork.Repository<AppUser>().GetFirstOrDefault(x =>x.Email == appUser);
            //initialize a class that contains a list of post that will belong to the currrent user 
            var nuListOfUserPost = new ListOfUserPost();
            //Creating an Id for the above list
            nuListOfUserPost.PostOwner = CurrentUser;
            //initializing the actual list of post that belongs to the current user
            nuListOfUserPost.CurrentUserPost = new List<Post>();
            //initializing a post
            var actualNewPost = new Post();
            actualNewPost.Comments = new List<Comment>();
            actualNewPost.LikeStatus = new List<LikeOrNot>();
                if(post.file != null)
                {
                    actualNewPost.Photos = new List<Photo>();
                    actualNewPost.Videos = new List<Videos>();
                    foreach(var item in post.file)
                    {               
                        if(item.ContentType == "video/mp4")
                        {
                            var info = await _videoService.SaveVideo(item);
                            var nuVid = new Videos
                            {
                                PostVideos = info
                            };
                            if(actualNewPost.Videos.Count == 0)
                                nuVid.IsMain = true;
                            actualNewPost.Videos.Add(nuVid);
                        } 
                        if(item.ContentType != "video/mp4")
                        {
                            var pics = await _photoService.AddPhotoAsync(item);
                            var nuPics = new Photo()
                            {
                                ApperUserPics = CurrentUser,
                                PhotoUrl = pics.SecureUrl.AbsoluteUri ?? null,
                                PublicId = pics.PublicId ?? null,
                            };
                             if(actualNewPost.Photos.Count == 0)
                                nuPics.IsMain = true;
                            actualNewPost.Photos.Add(nuPics);
                        }   
                        actualNewPost.AppUser = CurrentUser;
                        actualNewPost.IsMainComment = post.Comments;     
                        var nuLike = new LikeOrNot
                        {
                            LikeStatus = "New Post"
                        };
                        actualNewPost.LikeStatus.Add(nuLike);                   
                    }
                    nuListOfUserPost.CurrentUserPost.Add(actualNewPost);  
                    _unitOfWork.Repository<ListOfUserPost>().Add(nuListOfUserPost);
                    await _unitOfWork.Complete(); 
                    return actualNewPost; 
                }
            actualNewPost.AppUser = CurrentUser;
            actualNewPost.IsMainComment = post.Comments;  
            var onlyNuLike = new LikeOrNot
            {
                LikeStatus = "New Post"
            };
            actualNewPost.LikeStatus.Add(onlyNuLike);
            nuListOfUserPost.CurrentUserPost.Add(actualNewPost);
            _unitOfWork.Repository<ListOfUserPost>().Add(nuListOfUserPost);
            await _unitOfWork.Complete();
            return actualNewPost;
        }
    }
}