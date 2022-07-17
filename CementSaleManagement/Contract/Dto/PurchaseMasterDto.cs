using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PurchaseMasterDto : EntityDto
    {
        public DateTime PurchaseDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public int SupplierMasterId { get; set; }

        public bool IsActive { get; set; }
    }
}
