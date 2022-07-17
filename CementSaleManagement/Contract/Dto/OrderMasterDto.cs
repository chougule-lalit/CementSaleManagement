using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class OrderMasterDto : EntityDto
    {
        public DateTime OrderDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public int CustomerMasterId { get; set; }

        public bool IsActive { get; set; }
    }
}
