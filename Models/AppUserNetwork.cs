using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class AppUserNetwork
    {
        [Key]
        public int Id { get; set; }
        public ICollection<AppUser> Friends { get; set; }
    }
}