using System.ComponentModel.DataAnnotations;

namespace WhiteUnity.DataAccess.Models
{
    public class PackageDependencyModel
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(150)]
        [Required]
        public string Package { get; set; }
        
        public string Version { get; set; }
    }
}