using System.Threading.Tasks;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface IPackageUpdateService
    {
        Task<PackageInfoDto> Update(PackageInfoDto info);
    }
}