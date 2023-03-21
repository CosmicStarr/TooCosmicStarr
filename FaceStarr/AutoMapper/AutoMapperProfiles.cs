using Models.DTOs;

namespace FaceStarr.AutoMapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Post,GetPostDTO>().ForPath(x=>x.Photo,o=>o.MapFrom(s=>s.Photos.PhotoUrl))
                                        .ForPath(x=>x.Comments,o=>o.MapFrom(s=>s.Comments.ActualComment))
                                        .ForPath(x=>x.LikeStatus,o=>o.MapFrom(s=>s.LikeStatus.LikeStatus))
                                        .ForPath(x=>x.Videos,o=>o.MapFrom(s=>s.Videos.PostVideos))
                                        .ForPath(x=>x.AppUser,o=>o.MapFrom(s=>s.AppUser.Email));
            CreateMap<Post,PostDTO>().ReverseMap()
                                     .ForPath(x=>x.Photos.file,o=>o.Ignore())
                                     .ForPath(x=>x.Comments.ActualComment,o=>o.MapFrom(s=>s.Comments));
            CreateMap<Post,PostDTO>().ForPath(x=>x.file,o=>o.Ignore())
                                     .ForPath(x=>x.Comments,o=>o.MapFrom(s=>s.Comments.ActualComment));
            CreateMap<Photo,PhotoDTO>().ReverseMap().ForPath(x=>x.file,o=>o.Ignore());
            CreateMap<Photo,PhotoDTO>();
            CreateMap<LikeOrNot,LikeStatusDTO>().ForMember(x =>x.LikeStatus,o=>o.MapFrom(x=>x.LikeStatus));
            CreateMap<Comment,CommentDTO>().ForMember(x =>x.ActualCommentDTO,o=>o.MapFrom(x=>x.ActualComment));
            CreateMap<Comment,CommentDTO>().ReverseMap().ForMember(x =>x.ActualComment,o=>o.MapFrom(x=>x.ActualCommentDTO));
            CreateMap<RegisterModel,RegisterModelDTO>();
            CreateMap<LoginModel,LoginDTO>();
        }
    }
}