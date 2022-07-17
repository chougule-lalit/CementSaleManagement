using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class EnquiryDto
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Remark { get; set; }
    }

    public class GetEnquiryInputDto : PagedResultInput
    {

    }
}
