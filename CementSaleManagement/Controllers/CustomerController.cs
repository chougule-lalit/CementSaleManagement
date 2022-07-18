using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class CustomerController : ICustomerMasterAppService
    {
        private readonly ICustomerMasterAppService _customerMasterAppService;

        public CustomerController(ICustomerMasterAppService customerMasterAppService)
        {
            _customerMasterAppService = customerMasterAppService;
        }

        //[HttpPost]
        //[Route("createOrUpdate")]
        public virtual Task CreateOrUpdate(CustomerMasterDto input)
        {
            return _customerMasterAppService.CreateOrUpdate(input);
        }

        //[HttpDelete]
        //[Route("delete/{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _customerMasterAppService.DeleteAsync(id);
        }

        //[HttpPost]
        //[Route("fetchCustomerMasterList")]
        public virtual Task<PagedResultDto<CustomerMasterDto>> FetchCustomerMasterListAsync(GetCustomerMasterInputDto input)
        {
            return _customerMasterAppService.FetchCustomerMasterListAsync(input);
        }

        //[HttpGet]
        //[Route("get/{id}")]
        public virtual Task<CustomerMasterDto> GetAsync(int id)
        {
            return _customerMasterAppService.GetAsync(id);
        }

        //[HttpGet]
        //[Route("getCustomerMasterDropdown/{id}")]
        public virtual Task<List<CustomerMasterDropdownDto>> GetCustomerMasterDropdownAsync()
        {
            return _customerMasterAppService.GetCustomerMasterDropdownAsync();
        }
    }
}
