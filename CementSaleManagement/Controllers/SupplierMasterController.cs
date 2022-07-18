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
    public class SupplierMasterController : ISupplierMasterAppService
    {
        private readonly ISupplierMasterAppService _supplierMasterAppService;

        public SupplierMasterController(ISupplierMasterAppService supplierMasterAppService)
        {
            _supplierMasterAppService = supplierMasterAppService;
        }

        //[HttpPost]
        //[Route("createOrUpdate")]
        public virtual Task CreateOrUpdate(SupplierMasterDto input)
        {
            return _supplierMasterAppService.CreateOrUpdate(input);
        }

        //[HttpDelete]
        //[Route("delete/{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _supplierMasterAppService.DeleteAsync(id);
        }

        //[HttpPost]
        //[Route("fetchSupplierMasterList")]
        public virtual Task<PagedResultDto<SupplierMasterDto>> FetchSupplierMasterListAsync(GetSupplierMasterInputDto input)
        {
            return _supplierMasterAppService.FetchSupplierMasterListAsync(input);
        }

        //[HttpGet]
        //[Route("get/{id}")]
        public virtual Task<SupplierMasterDto> GetAsync(int id)
        {
            return _supplierMasterAppService.GetAsync(id);
        }

        //[HttpGet]
        //[Route("getSupplierMasterDropdown/{id}")]
        public virtual Task<List<SupplierMasterDropdownDto>> GetSupplierMasterDropdownAsync()
        {
            return _supplierMasterAppService.GetSupplierMasterDropdownAsync();
        }
    }
}
