using System;
using System.Collections.Generic;
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
        
        [MaxLength(300)]
        [Required]
        public string DisplayName { get; set; }

        [MaxLength(250)]
        [Required]
        public string ProjectUrl { get; set; }

        [MaxLength(250)]
        [Required]
        public string UrlForManifest { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Version { get; set; }

        public IList<PackageDependencyModel> Dependencies { get; set; }

        public string Category { get; set; }

        public string Unity { get; set; }

        public DateTime AddedTimestamp { get; set; }

        
        public int LooksCount { get; set; }
    }
}