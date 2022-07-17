using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class PurchaseMaster : BaseEntity
    {
        public DateTime PurchaseDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public SupplierMaster SupplierMaster { get; set; }

        public int SupplierMasterId { get; set; }

        public bool IsActive { get; set; }
    }
}
