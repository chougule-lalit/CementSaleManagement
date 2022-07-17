using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PruchaseDetailDto : EntityDto
    {
        public int PurchaseMasterId { get; set; }

        public int ProductMasterId { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
