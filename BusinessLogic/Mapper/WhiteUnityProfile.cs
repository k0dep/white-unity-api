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
        }
    }
}