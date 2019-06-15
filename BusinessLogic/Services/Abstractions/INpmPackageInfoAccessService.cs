using System.Threading.Tasks;
using WhiteUnity.BusinessLogic.Objects;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface INpmPackageInfoAccessService
    {
        Task<PackageMetaInfo> TryGetPackageInfo(string url);
    }
}