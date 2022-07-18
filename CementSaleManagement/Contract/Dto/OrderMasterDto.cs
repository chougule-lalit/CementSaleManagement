using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class OrderMasterDto : EntityDto
    {
        public DateTime OrderDate { get; set; }

        public DateTime CancelDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public int UserMasterId { get; set; }

        public bool IsActive { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class GetOrderInputDto : PagedResultInput
    {
        public DateTime? OrderDate { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime CancelDate { get; set; }

        public int ItemCount { get; set; }

        public decimal Amount { get; set; }

        public string CustomerName { get; set; }

        public bool IsActive { get; set; }
    }
}
