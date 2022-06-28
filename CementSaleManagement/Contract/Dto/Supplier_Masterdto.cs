using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class Supplier_Masterdto
    {

        public int Id { get; set; }

        public string Supplier_Name { get; set; }

        public string Supplier_Address { get; set; }

        public string Product_Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class SupplierCreateUpdateDto
    {
        public int? Id { get; set; }

        public string Supplier_Name { get; set; }

        public string Supplier_Address { get; set; }

        public string Product_Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

    public class GetSupplierInput
    {
        public int MaxResultCount { get; set; } = 10;

        public int SkipCount { get; set; }

        public string Search { get; set; }
    }
}

