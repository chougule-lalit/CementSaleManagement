using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class PurchaseMasterDto : EntityDto
    {
        public DateTime PurchaseDate { get; set; }

        public DateTime CancelDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public int UserMasterId { get; set; }

        public bool IsActive { get; set; }

        public List<PurchaseDetailDto> PurchaseDetails { get; set; }
    }

    public class GetPurchaseInputDto : PagedResultInput
    {
        public DateTime? PurchaseDate { get; set; }
    }

    public class PurchaseDto
    {
        public int PurchaseId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime CancelDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public string SupplierName { get; set; }

        public bool IsActive { get; set; }
    }
}
