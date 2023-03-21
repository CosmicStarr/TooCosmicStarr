using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get {return FirstName + " " + LastName;}}
        public ListOfUserPost AppUserPost { get; set; }
        public AppUserNetwork NetworkOfFriends { get; set; }
    }
}