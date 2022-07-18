using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class ProductMaster : BaseEntity
    {
        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public decimal Price { get; set; }
    }
}
