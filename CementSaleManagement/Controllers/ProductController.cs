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
    public class ProductController : IProductMasterAppService
    {
        private readonly IProductMasterAppService _productMasterAppService;

        public ProductController(IProductMasterAppService productMasterAppService)
        {
            _productMasterAppService = productMasterAppService;
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task CreateOrUpdate(ProductMasterDto input)
        {
            return _productMasterAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(int id)
        {
            return _productMasterAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("fetchProductMasterList")]
        public virtual Task<PagedResultDto<ProductMasterDto>> FetchProductMasterListAsync(GetProductMasterInputDto input)
        {
            return _productMasterAppService.FetchProductMasterListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<ProductMasterDto> GetAsync(int id)
        {
            return _productMasterAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("getProductMasterDropdown")]
        public virtual Task<List<ProductMasterDropdownDto>> GetProductMasterDropdownAsync()
        {
            return _productMasterAppService.GetProductMasterDropdownAsync();
        }
    }
}
