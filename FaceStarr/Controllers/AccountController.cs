using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FaceStarr.GlobalErrorHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Models.DTOs;
using Newtonsoft.Json;

namespace FaceStarr.Controllers
{
    
    public class AccountController:BaseController
    {
        private readonly IApplicationUser _applicationUser;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICreatePost _createPost;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILikeAPost _likeAPost;

        public AccountController(
        IMapper mapper,
        UserManager<AppUser> userManager,
        IApplicationUser applicationUser,
        ICreatePost createPost,
        IUnitOfWork unitOfWork,
        ILikeAPost likeAPost)
        {
            _applicationUser = applicationUser;
            _userManager = userManager;
            _mapper = mapper;
            _createPost = createPost;
            _unitOfWork = unitOfWork;
            _likeAPost = likeAPost;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterModelDTO>> Register(RegisterModel registerModel)
        {
            if(await _userManager.Users.AnyAsync(x =>x.Email == registerModel.Email)) return BadRequest("This Email Already Exist!");
            var mappedObj =  _mapper.Map<RegisterModel,RegisterModelDTO>(registerModel);
            var user = await _applicationUser.SiginUp(mappedObj);
            if(user == null) return BadRequest(new ErrorResponse(400));
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login(LoginModel loginModel)
        {
            var mappedObj = _mapper.Map<LoginModel,LoginDTO>(loginModel);
            var loginUser = await _applicationUser.Login(mappedObj);
            if(loginUser == null) return NotFound(new ErrorResponse(404));
            return loginUser;
        }
        
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)] 
        [DisableRequestSizeLimit] 
        [Consumes("multipart/form-data")]
        [HttpPost("CreatePost")]
        // [Authorize]
        public async Task <ActionResult<PostDTO>> CreatePost([FromForm]PostDTO post)
        {          
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            var obj = await _createPost.CreatePost(post,currentUser);
            var mappedObj =  _mapper.Map<Post,PostDTO>(obj);
            return Ok(mappedObj);          
        }
  
        [HttpGet("GlobalPost")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPost()
        {
            var Data = await _unitOfWork.Repository<Post>().GetAll(null,null,"Photos,Videos,Comments,LikeStatus,AppUser");
            return Ok(_mapper.Map<IEnumerable<Post>,IEnumerable<GetPostDTO>>(Data));
        }

        [HttpPost("LikedPost/{id}")]
        public async Task<ActionResult<int>> AmountOFLikesOnPost(int id, LikesDTO like)
        {
            var currentUser = HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            var numOfLikes = await _likeAPost.PostALike(id,like,currentUser);
            return Ok(numOfLikes);
        }
    }
}