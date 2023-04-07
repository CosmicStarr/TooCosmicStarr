namespace Data.Classes
{
    public class LikeAPost : ILikeAPost
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikeAPost(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;    
        }
        public async Task<Post> PostALike(int id ,LikesDTO like,string currentUser)
        {
            //Get the Current User
            var CurrentUser = await _unitOfWork.Repository<AppUser>().GetFirstOrDefault(x=>x.Email == currentUser);
            //Get the post the Current User Liked
            var Data = await _unitOfWork.Repository<Post>().GetFirstOrDefault(x=>x.Id == id,"Photos,Videos,Comments,LikeStatus,AppUser");
            var info = Data.LikeStatus.FirstOrDefault(x=>x.AppUserLikes == CurrentUser);
            //Create a new Like object
            var nuLike = new LikeOrNot
            {
                Likes = like.Like,
                LikeStatus = like.LikeStatusDTO,
                AppUserLikes = CurrentUser,
            };
            if(info == null)
            {
                //Add the new like object to List of likes that belongs to the Post the Current User liked
                Data.LikeStatus.Add(nuLike);
                await _unitOfWork.Complete(); 
                return Data;
            }
            else
            {
                if(nuLike.AppUserLikes == info.AppUserLikes)
                {
                    Data.LikeStatus.Remove(info);
                    await _unitOfWork.Complete(); 
                    return Data;
                }
            }
            return null;
        }
    }
}