using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class OrderCancellationMasterDto : EntityDto
    {
        public DateTime CancelDate { get; set; }

        public int OrderMasterId { get; set; }
    }
}
