using System.Linq;
using AutoMapper;
using WhiteUnity.DataAccess.Models;

namespace WhiteUnity.BusinessLogic
{
    public class WhiteUnityProfile : Profile
    {
        public WhiteUnityProfile()
        {
            CreateMap<PackageInfoDto, PackageModel>()
                .ForMember(c => c.Dependencies,
                    e => e.MapFrom(c => c.Dependencies.Select(s => new PackageDependencyModel()
                    {
                        Package = s,
                        Version = "0.0.0"
                    })));

            CreateMap<PackageModel, PackageInfoDto>()
                .ForMember(c => c.Dependencies, e => e.MapFrom(d => d.Dependencies.Select(c => c.Package)));
            
        }
    }
    
    public class WhiteUnityProfile_Npm : Profile
    {
        public WhiteUnityProfile_Npm()
        {
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<NpmPackageObject, PackageInfoDto>()
                .ForMember(c => c.Dependencies, e => e.MapFrom(c => c.dependencies.Select(s => s.Key).ToArray()));
        }
    }
}