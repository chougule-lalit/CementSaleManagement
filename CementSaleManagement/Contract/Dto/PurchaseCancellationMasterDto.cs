using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PurchaseCancellationMasterDto
    {
        public DateTime CancelDate { get; set; }

        public int PurchaseMasterId { get; set; }
    }
}
