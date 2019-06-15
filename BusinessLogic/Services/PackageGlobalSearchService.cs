using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WhiteUnity.BusinessLogic.Abstractions;

namespace WhiteUnity.BusinessLogic
{
    public class PackageGlobalSearchService : IPackageGlobalSearchService
    {
        private readonly INpmPackageInfoAccessService _packageInfoAccess;
        private readonly IMapper _mapper;

        public PackageGlobalSearchService(INpmPackageInfoAccessService packageInfoAccess, IMapper mapper)
        {
            _packageInfoAccess = packageInfoAccess;
            _mapper = mapper;
        }

        public async Task<PackageInfoDto> GlobalSearch(string gitUrl)
        {
            var pakcageData = await _packageInfoAccess.TryGetPackageInfo(gitUrl);
            var packageInfo = pakcageData.Info;
            var package = _mapper.Map<NpmPackageObject, PackageInfoDto>(packageInfo);

            package.ProjectUrl = gitUrl;
            package.UrlForManifest = gitUrl;
            package.Versions = pakcageData.Branches.Union(pakcageData.Tags).ToArray();

            return package;
        }
    }
}