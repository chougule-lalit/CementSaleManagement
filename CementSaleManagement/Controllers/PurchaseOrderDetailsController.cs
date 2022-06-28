using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace CementSaleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseOrderDetailsController// : IProductOrderDetailsAppService
    {
      
        private readonly IProductOrderDetailsAppService _IProductOrderDetailsAppService;

            public PurchaseOrderDetailsController(IProductOrderDetailsAppService IProductOrderDetailsAppService)
            {
                _IProductOrderDetailsAppService = IProductOrderDetailsAppService;
            }

            [HttpPost]
            [Route("CreateOrUpdateProductorder")]
            public virtual Task CreateOrUpdateProductorder(ProductOrderDetailsDto input)
            {
                return _IProductOrderDetailsAppService.createpurchase(input);
            }

        
        //[HttpDelete]
        //[Route("delete")]
        //public virtual Task DeleteUserAsync(int id)
        //{
        //    return _SuppplierMasterAppService.DeleteSupplierAsync(id);
        //}

        //[HttpGet]
        //[Route("getUser")]
        //public virtual Task<Supplier_Masterdto> GetSupplierAsync(int id)
        //{
        //    //return _SuppplierMasterAppService.GetSupplierAsync(id);
        //}

        // [HttpPost]
        // [Route("fetchUserList")]
        //public virtual Task<PagedResultDto<Supplier_Masterdto>> FetchSupplierListAsync(GetSupplierInput input)
        //{
        //    return _SuppplierMasterAppService.FetchSupplerListAsync(input);
        //}

    }
}
