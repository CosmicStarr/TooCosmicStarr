using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        
        public DbSet<Post> GetPosts { get; set; }
        public DbSet<AppUser> GetUsers { get; set; }
        public DbSet<ListOfUserPost> GetListOfUserPosts { get; set; }
        public DbSet<AppUserNetwork> GetAppUserNetwork { get; set; }
        public DbSet<Photo> GetPhotos { get; set; }
        public DbSet<Videos> GetVideos { get; set; }
        public DbSet<Comment> GetComments { get; set; }
    }
}