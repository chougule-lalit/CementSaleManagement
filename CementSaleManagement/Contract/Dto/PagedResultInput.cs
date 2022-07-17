using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PagedResultInput
    {
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; }

        public string Search { get; set; }
    }
}
