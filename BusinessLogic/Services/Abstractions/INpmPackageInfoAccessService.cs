using System.Threading.Tasks;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface INpmPackageInfoAccessService
    {
        Task<NpmPackageObject> TryGetPackageInfo(string url);
    }
}