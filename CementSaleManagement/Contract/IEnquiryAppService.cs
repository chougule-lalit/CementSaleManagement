using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IEnquiryAppService
    {
        Task CreateOrUpdate(EnquiryDto input);
        Task<EnquiryDto> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<PagedResultDto<EnquiryDto>> FetchEnquiryListAsync(GetEnquiryInputDto input);
    }
}
