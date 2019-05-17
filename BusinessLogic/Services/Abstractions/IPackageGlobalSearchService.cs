using System.Threading.Tasks;
using AutoMapper;
using WhiteUnity.BusinessLogic;
using WhiteUnity.DataAccess.Abstraction;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface IPackageGlobalSearchService
    {
        Task<PackageInfoDto> GlobalSearch(string gitUrl);
    }
}