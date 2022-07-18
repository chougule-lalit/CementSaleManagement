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
    public class OrderController : IOrderAppService
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        [Route("cancelOrder")]
        public virtual Task<bool> CancelOrder(int id)
        {
            return _orderAppService.CancelOrder(id);
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task<bool> CreateOrUpdate(OrderMasterDto input)
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
        [Route("fetchCancelledOrderList")]
        public virtual Task<PagedResultDto<OrderDto>> FetchCancelledOrderListAsync(GetOrderInputDto input)
        {
            return _orderAppService.FetchCancelledOrderListAsync(input);
        }

        [HttpPost]
        [Route("fetchOrderList")]
        public virtual Task<PagedResultDto<OrderDto>> FetchOrderListAsync(GetOrderInputDto input)
        {
            return _orderAppService.FetchOrderListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<OrderMasterDto> GetAsync(int id)
        {
            return _orderAppService.GetAsync(id);
        }
    }
}
