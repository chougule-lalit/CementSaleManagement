using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class RoleMasterDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }

    public class GetRoleInputDto : PagedResultInput
    {

    }

    public class RoleDropdownDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
