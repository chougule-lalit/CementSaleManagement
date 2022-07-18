using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface ICustomerMasterAppService
    {
        Task CreateOrUpdate(CustomerMasterDto input);
        Task<CustomerMasterDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<CustomerMasterDto>> FetchCustomerMasterListAsync(GetCustomerMasterInputDto input);
        Task<List<CustomerMasterDropdownDto>> GetCustomerMasterDropdownAsync();
    }
}
