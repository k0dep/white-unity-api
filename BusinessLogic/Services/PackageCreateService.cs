using System;
using System.Threading.Tasks;
using AutoMapper;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.DataAccess.Abstraction;
using WhiteUnity.DataAccess.Models;

namespace WhiteUnity.BusinessLogic
{
    public class PackageCreateService : IPackageCreateService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PackageModel> _packageRepo;

        public PackageCreateService(IMapper mapper, IRepository<PackageModel> packageRepo)
        {
            _mapper = mapper;
            _packageRepo = packageRepo;
        }

        public async Task<int> Create(PackageInfoDto info)
        {
            var model = _mapper.Map<PackageModel>(info);
            model.AddedTimestamp = DateTime.UtcNow;

            await _packageRepo.Add(model);

            return model.Id;
        }
    }
}