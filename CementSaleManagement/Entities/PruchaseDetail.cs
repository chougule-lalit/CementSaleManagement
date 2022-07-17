using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class PruchaseDetail : BaseEntity
    {
        public PurchaseMaster PurchaseMaster { get; set; }

        public int PurchaseMasterId { get; set; }

        public ProductMaster ProductMaster { get; set; }

        public int ProductMasterId { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
