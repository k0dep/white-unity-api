using System;

namespace WhiteUnity.BusinessLogic
{
    public class PackageInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ProjectUrl { get; set; }
        public string UrlForManifest { get; set; }
    }
}