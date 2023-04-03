using Models.DTOs;

namespace FaceStarr.AutoMapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Post,GetPostDTO>().ForPath(x=>x.Photos,o=>o.MapFrom(s=>s.Photos))
                                        .ForPath(x=>x.IsMainComment,o=>o.MapFrom(s=>s.IsMainComment))
                                        .ForPath(x=>x.Videos,o=>o.MapFrom(s=>s.Videos))
                                        .ForPath(x=>x.AppUser,o=>o.MapFrom(s=>s.AppUser.Email));
            CreateMap<Videos,VideoDTO>().ForPath(x=>x.AppUserVideos,o=>o.MapFrom(s=>s.AppUserVideos));                                                                                         
            CreateMap<Post,PostDTO>().ReverseMap()
                                     .ForPath(x=>x.Photos,o=>o.Ignore())
                                     .ForPath(x=>x.Comments,o=>o.MapFrom(s=>s.Comments));
            CreateMap<Post,PostDTO>().ForPath(x=>x.file,o=>o.Ignore())
                                     .ForPath(x=>x.Comments,o=>o.MapFrom(s=>s.Comments));
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