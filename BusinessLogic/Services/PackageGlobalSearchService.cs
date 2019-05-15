using System.Threading.Tasks;
using AutoMapper;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.BusinessLogic.Services;

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
            var packageInfo = await _packageInfoAccess.TryGetPackageInfo(gitUrl);
            var package = _mapper.Map<NpmPackageObject, PackageInfoDto>(packageInfo);

            package.ProjectUrl = gitUrl;
            package.UrlForManifest = gitUrl;

            return package;
        }
    }
}