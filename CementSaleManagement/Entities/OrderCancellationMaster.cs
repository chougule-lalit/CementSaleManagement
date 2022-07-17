using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class OrderCancellationMaster : BaseEntity
    {
        public DateTime CancelDate { get; set; }

        public OrderMaster OrderMaster { get; set; }

        public int OrderMasterId { get; set; }
    }
}
