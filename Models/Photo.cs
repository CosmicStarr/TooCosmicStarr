using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace Models
{
    [Table("Pictures")]
    public class Photo
    {
        [Key]
        public int Id { get; set; } 
        public bool IsMain { get; set; }
        public string PhotoUrl { get; set; }
        [NotMapped]
        public ICollection<IFormFile> file { get; set; }
        public string PublicId { get; set; }
        public AppUser ApperUserPics { get; set; }
    }
}