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
    public class PurchaseController : IPurchaseAppService
    {
        private readonly IPurchaseAppService _orderAppService;

        public PurchaseController(IPurchaseAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        [Route("cancelPurchase")]
        public virtual Task<bool> CancelPurchase(int id)
        {
            return _orderAppService.CancelPurchase(id);
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task<bool> CreateOrUpdate(PurchaseMasterDto input)
        {
            return _orderAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task<bool> DeleteAsync(int id)
        {
            return _orderAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("fetchCancelledPurchaseList")]
        public virtual Task<PagedResultDto<PurchaseDto>> FetchCancelledPurchaseListAsync(GetPurchaseInputDto input)
        {
            return _orderAppService.FetchCancelledPurchaseListAsync(input);
        }

        [HttpPost]
        [Route("fetchPurchaseList")]
        public virtual Task<PagedResultDto<PurchaseDto>> FetchPurchaseListAsync(GetPurchaseInputDto input)
        {
            return _orderAppService.FetchPurchaseListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<PurchaseMasterDto> GetAsync(int id)
        {
            return _orderAppService.GetAsync(id);
        }
    }
}
