using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ListOfUserPost
    {
        public string Id { get; set; }
        public ICollection<Post> CurrentUserPost { get; set; }
    }
}