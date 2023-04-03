using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ILikeAPost
    {
        Task<int> PostALike(int id,LikesDTO like,string postOwer);
    }
}