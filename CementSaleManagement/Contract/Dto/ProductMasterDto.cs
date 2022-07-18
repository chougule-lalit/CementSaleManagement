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

        public decimal Price { get; set; }
    }

    public class GetProductMasterInputDto : PagedResultInput
    {

    }

    public class ProductMasterDropdownDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
