using System.Collections.Generic;
using WhiteUnity.BusinessLogic.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteUnity.BusinessLogic.Abstraction
{
    public interface IPagingService
    {
        Task<PagingResultDto<TResult>> Paging<TResult>(IQueryable<TResult> collection, PageRequestDto pageParams);
    }
}