using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class SupplierMasterDto : EntityDto
    {
        public string SupplierName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int UserMasterId { get; set; }
    }

    public class GetSupplierMasterInputDto : PagedResultInput
    {

    }

    public class SupplierMasterDropdownDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
