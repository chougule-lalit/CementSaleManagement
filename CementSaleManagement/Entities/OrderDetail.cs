using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class OrderDetail : BaseEntity
    {
        public OrderMaster OrderMaster { get; set; }

        public int OrderMasterId { get; set; }

        public ProductMaster ProductMaster { get; set; }

        public int ProductMasterId { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }
    }
}
