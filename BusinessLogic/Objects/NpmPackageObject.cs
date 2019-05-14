using System.Collections.Generic;

namespace WhiteUnity.BusinessLogic.Objects
{
    public class NpmPackageObject
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string unity { get; set; }
        public string[] keywords { get; set; }
        public string category { get; set; }
        public Dictionary<string, string> dependencies { get; set; }
    }
}