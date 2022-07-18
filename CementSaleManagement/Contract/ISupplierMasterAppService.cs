using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface ISupplierMasterAppService
    {
        Task CreateOrUpdate(SupplierMasterDto input);
        Task<SupplierMasterDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<SupplierMasterDto>> FetchSupplierMasterListAsync(GetSupplierMasterInputDto input);
        Task<List<SupplierMasterDropdownDto>> GetSupplierMasterDropdownAsync();
    }
}
