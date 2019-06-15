using System.Linq;
using AutoMapper;
using WhiteUnity.DataAccess.Models;

namespace WhiteUnity.BusinessLogic
{
    public class WhiteUnityProfile : Profile
    {
        public WhiteUnityProfile()
        {
            CreateMap<PackageInfoDto, PackageModel>();

            CreateMap<PackageModel, PackageInfoDto>();
            
            CreateMap<PackageVersionModel, string>()
                .ConvertUsing(t => t.Version);
            CreateMap<string, PackageVersionModel>()
                .ForMember(t => t.Version, e => e.MapFrom(d => d));

            CreateMap<PackageDependencyModel, string>()
                .ConvertUsing(t => t.Package);
            CreateMap<string, PackageDependencyModel>()
                .ForMember(t => t.Version, e => e.MapFrom(d => "0.0.0"))
                .ForMember(t => t.Package, e => e.MapFrom(d => d));
        }
    }
    
    public class WhiteUnityProfile_Npm : Profile
    {
        public WhiteUnityProfile_Npm()
        {
            SourceMemberNamingConvention = new PascalCaseNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            CreateMap<NpmPackageObject, PackageInfoDto>()
                .ForMember(t => t.Dependencies, s => s.MapFrom(e => e.dependencies.Keys))
                .ForMember(t => t.Versions, e => e.Ignore());
        }
    }
}