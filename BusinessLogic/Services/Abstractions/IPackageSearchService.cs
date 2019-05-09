using System.Threading.Tasks;
using WhiteUnity.BusinessLogic.Objects;

namespace WhiteUnity.BusinessLogic.Abstractions
{
    public interface IPackageSearchService
    {
        Task<PackageInfoDto> BestMatch(string name);

        Task<PagingResultDto<PackageInfoDto>> Search(PageRequestDto page, string query);
    }
}