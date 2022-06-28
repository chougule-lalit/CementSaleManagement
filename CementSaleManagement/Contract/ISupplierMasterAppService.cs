using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface ISupplierMasterAppService
    {

        //Task CreateOrUpdateSupplier(SupplierCreateUpdateDto input);
        Task CreateOrUpdateSupplier(SupplierCreateUpdateDto input);
        //Task<PagedResultDto<Supplier_Masterdto>> FetchSupplerListAsync(GetSupplierInput input);

        //Task<PagedResultDto<Supplier_Masterdto>> FetchSuplierListAsync(GetSupplierInput input);

        //Task<Supplier_Masterdto> GetSupplierAsync(int id);

        //   Task DeleteSupplierAsync(int id);
    }
}
