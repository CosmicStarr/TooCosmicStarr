using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Models.DTOs;

namespace FaceStarr.Controllers
{
   
    public class PublicWallController:BaseController
    {
        private readonly IMapper _mapper;
        
        public PublicWallController(IMapper mapper)
        {
            _mapper = mapper;
        }


    }
}