using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PagedResultDto<T> : ListResultDto<T>
    {
        public PagedResultDto() { }

        public PagedResultDto(long totalCount, IReadOnlyList<T> items) { }

        public long TotalCount { get; set; }
    }

    public class ListResultDto<T>
    {
        public ListResultDto() { }

        public ListResultDto(IReadOnlyList<T> items) { }

        public IReadOnlyList<T> Items { get; set; }
    }
}
