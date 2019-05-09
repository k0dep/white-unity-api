using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteUnity.BusinessLogic.Abstraction;
using WhiteUnity.BusinessLogic.Objects;

namespace WhiteUnity.BusinessLogic
{
    public class PagingService : IPagingService
    {
        public int PageDefaultCount = 15;

        public PagingService()
        {
        }
        
        public async Task<PagingResultDto<TResult>> Paging<TResult>(IQueryable<TResult> collection, PageRequestDto pageParams)
        {
            var count = collection.Count();
            var skiping = (pageParams.Count ?? PageDefaultCount) * (pageParams.Page ?? 0);
            var taking = pageParams.Count ?? PageDefaultCount;
            var resultCollection = await collection.Skip(skiping).Take(taking).ToListAsync();
            return new PagingResultDto<TResult>(count, pageParams.Count ?? PageDefaultCount, pageParams.Page ?? 0, resultCollection);
        }
    }
}