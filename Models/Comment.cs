using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string ActualComment { get; set; }
        public AppUser AppUserComments { get; set; }
    }
}