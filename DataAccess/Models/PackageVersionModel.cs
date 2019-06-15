using System.ComponentModel.DataAnnotations;

namespace WhiteUnity.DataAccess.Models
{
    public class PackageVersionModel
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(150)]
        [Required]
        public string Version { get; set; }
    }
}