using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace Data.Interfaces
{
    public interface ICreatePost
    {
        Task<Post> CreatePost(PostDTO post, string appUser);
    }
}