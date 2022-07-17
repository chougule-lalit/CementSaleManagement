using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class PurchaseCancellationMaster : BaseEntity
    {
        public DateTime CancelDate { get; set; }

        public PurchaseMaster PurchaseMaster { get; set; }

        public int PurchaseMasterId { get; set; }
    }
}
