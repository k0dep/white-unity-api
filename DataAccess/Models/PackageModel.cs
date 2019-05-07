using System;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionsTest.DataAccess.Models
{
    public class PackageModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        [Required]
        public string ProjectUrl { get; set; }

        [MaxLength(250)]
        [Required]
        public string UrlForManifest { get; set; }

        public DateTime AddedTimestamp { get; set; }
        public int LooksCount { get; set; }

        public int LooksCount2 { get; set; }
    }
}