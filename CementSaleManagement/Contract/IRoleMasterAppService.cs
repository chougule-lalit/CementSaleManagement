using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IRoleMasterAppService
    {
        Task CreateOrUpdateAsync(RoleMasterDto input);
        Task<RoleMasterDto> GetRoleAsync(int id);
        Task DeleteRoleAsync(int id);
        Task<PagedResultDto<RoleMasterDto>> FetchRolesListAsync(GetRoleInputDto input);
        Task<List<RoleDropdownDto>> GetRoleDropdownAsync();
    }
}
