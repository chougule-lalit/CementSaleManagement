using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class ProductMasterDto:EntityDto
    {
        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public int SupplierMasterId { get; set; }
    }
}
