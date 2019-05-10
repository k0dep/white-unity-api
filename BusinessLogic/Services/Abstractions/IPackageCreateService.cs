using System.Threading.Tasks;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface IPackageCreateService
    {
        Task<int> Create(PackageInfoDto info);
    }
}