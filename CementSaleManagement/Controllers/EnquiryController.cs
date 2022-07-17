using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnquiryController : IEnquiryAppService
    {
        private readonly IEnquiryAppService _enquiryAppService;

        public EnquiryController(IEnquiryAppService enquiryAppService)
        {
            _enquiryAppService = enquiryAppService;
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task CreateOrUpdate(EnquiryDto input)
        {
            return _enquiryAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _enquiryAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("fetchEnquiryList")]
        public virtual Task<PagedResultDto<EnquiryDto>> FetchEnquiryListAsync(GetEnquiryInputDto input)
        {
            return _enquiryAppService.FetchEnquiryListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<EnquiryDto> GetAsync(int id)
        {
            return _enquiryAppService.GetAsync(id);
        }
    }
}
