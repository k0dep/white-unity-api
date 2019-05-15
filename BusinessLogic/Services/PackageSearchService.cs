using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WhiteUnity.BusinessLogic.Abstraction;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.BusinessLogic.Objects;
using WhiteUnity.DataAccess.Abstraction;
using WhiteUnity.DataAccess.Models;

namespace WhiteUnity.BusinessLogic.Services
{
    public class PackageSearchService : IPackageSearchService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PackageModel> _packageRepo;
        private readonly IPagingService _paging;

        public PackageSearchService(IRepository<PackageModel> packageRepo, IMapper mapper, IPagingService paging)
        {
            _packageRepo = packageRepo;
            _mapper = mapper;
            _paging = paging;
        }

        public async Task<PackageInfoDto> BestMatch(string name)
        {
            return await _packageRepo.Get(p => string.Equals(p.Name, name))
                .ProjectTo<PackageInfoDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagingResultDto<PackageInfoDto>> Search(PackageSearchRequestDto filter)
        {
            var result = _packageRepo.Get(p => string.IsNullOrEmpty(filter.Name)
                                                || p.Name.Contains(filter.Name)
                                                || p.DisplayName.Contains(filter.Name))
                .ProjectTo<PackageInfoDto>(_mapper.ConfigurationProvider);
            return await _paging.Paging(result, filter);
        }
    }
}