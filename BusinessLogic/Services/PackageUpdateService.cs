using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.DataAccess.Abstraction;
using WhiteUnity.DataAccess.Models;

namespace WhiteUnity.BusinessLogic
{
    public class PackageUpdateService : IPackageUpdateService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PackageModel> _packageRepo;

        public PackageUpdateService(IMapper mapper, IRepository<PackageModel> packageRepo)
        {
            _mapper = mapper;
            _packageRepo = packageRepo;
        }


        public async Task<PackageInfoDto> Update(PackageInfoDto info)
        {
            var model = await _packageRepo.Get(p => p.Id == info.Id)
                .Include(p => p.Dependencies)
                .SingleOrDefaultAsync();

            if (model == null)
            {
                return null;
            }

            model.Dependencies = null;

            _mapper.Map(info, model);

            model = await _packageRepo.Update(model);

            return _mapper.Map<PackageInfoDto>(model);
        }
    }
}