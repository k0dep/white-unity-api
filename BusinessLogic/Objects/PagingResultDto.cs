using System.Collections.Generic;

namespace WhiteUnity.BusinessLogic.Objects
{
    public class PagingResultDto<TItem>
    {
        public int Count { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public IEnumerable<TItem> Items { get; set; }

        public PagingResultDto(int count, int itemsPerPage, int page, IEnumerable<TItem> items)
        {
            Count = count;
            ItemsPerPage = itemsPerPage;
            Page = page;
            Items = items;
        }
    }
}