using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class CustomerMasterDto : EntityDto
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int UserMasterId { get; set; }
    }
}
