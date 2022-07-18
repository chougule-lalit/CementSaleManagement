using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IPurchaseAppService
    {
        Task<bool> CreateOrUpdate(PurchaseMasterDto input);
        Task<PurchaseMasterDto> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto<PurchaseDto>> FetchPurchaseListAsync(GetPurchaseInputDto input);
        Task<PagedResultDto<PurchaseDto>> FetchCancelledPurchaseListAsync(GetPurchaseInputDto input);
        Task<bool> CancelPurchase(int id);
    }
}
