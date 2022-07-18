using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IOrderAppService
    {
        Task<bool> CreateOrUpdate(OrderMasterDto input);
        Task<OrderMasterDto> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<PagedResultDto<OrderDto>> FetchOrderListAsync(GetOrderInputDto input);
        Task<PagedResultDto<OrderDto>> FetchCancelledOrderListAsync(GetOrderInputDto input);
        Task<bool> CancelOrder(int id);
        Task<ExportToExcelDto> DownloadReportAsync();
        Task<ExportToExcelDto> DownloadCancelReportAsync();

    }
}
