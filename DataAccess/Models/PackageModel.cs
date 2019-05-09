using System;
using System.ComponentModel.DataAnnotations;

namespace WhiteUnity.DataAccess.Models
{
    public class PackageModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string FullName { get; set; }

        [MaxLength(250)]
        [Required]
        public string ProjectUrl { get; set; }

        [MaxLength(250)]
        [Required]
        public string UrlForManifest { get; set; }

        public DateTime AddedTimestamp { get; set; }
        
        public int LooksCount { get; set; }
    }
}