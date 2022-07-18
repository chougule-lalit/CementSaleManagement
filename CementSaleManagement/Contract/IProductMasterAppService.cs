using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IProductMasterAppService
    {
        Task CreateOrUpdate(ProductMasterDto input);
        Task<ProductMasterDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<ProductMasterDto>> FetchProductMasterListAsync(GetProductMasterInputDto input);
        Task<List<ProductMasterDropdownDto>> GetProductMasterDropdownAsync();

    }
}
