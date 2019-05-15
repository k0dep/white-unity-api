using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;

namespace WhiteUnity.BusinessLogic
{
    public class NpmPackageObject
    {
        [MapTo("Name")]
        public string name { get; set; }
        
        [MapTo("DisplayName")]
        public string displayName { get; set; }
        
        [MapTo("Version")]
        public string version { get; set; }
        
        [MapTo("Description")]
        public string description { get; set; }
        
        [MapTo("Unity")]
        public string unity { get; set; }
        
        public string[] keywords { get; set; }
        
        [MapTo("Category")]
        public string category { get; set; }
        
        [MapTo("Dependencies")]
        public Dictionary<string, string> dependencies { get; set; }
    }
}