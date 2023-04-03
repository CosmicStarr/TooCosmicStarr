namespace Data.Classes
{
    public class LikeAPost : ILikeAPost
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikeAPost(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;    
        }
        public async Task<int> PostALike(int id ,LikesDTO like,string currentUser)
        {
            //Get the Current User
            var CurrentUser = await _unitOfWork.Repository<AppUser>().GetFirstOrDefault(x=>x.Email == currentUser);
            //Get the post the Current User Liked
            var Data = await _unitOfWork.Repository<Post>().GetFirstOrDefault(x=>x.Id == id,"Photos,Videos,Comments,LikeStatus,AppUser");
            //Initialize the List of likes that belongs to the Post the Current User liked
            Data.LikeStatus = new List<LikeOrNot>();
            //Create a new Like object
            var nuLike = new LikeOrNot
            {
                Likes = like.Like,
                LikeStatus = like.LikeStatusDTO,
                AppUserLikes = CurrentUser
            };
            //Add the new like object to List of likes that belongs to the Post the Current User liked
            Data.LikeStatus.Add(nuLike);
            await _unitOfWork.Complete();
            return Data.LikeStatus.Count;
        }
    }
}