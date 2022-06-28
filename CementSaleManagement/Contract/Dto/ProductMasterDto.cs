using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class ProductMasterDto
    {
        public int? Id { get; set; }

        public string ProductName { get; set; }
    }
    public class ProductMasterCreateUpdateDto
    {
        public int? Id { get; set; }

        public string ProductName { get; set; }

        
    }

    public class GetProductInput
    {
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; }

        public string Search { get; set; }
    }




}
