using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace CementSaleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductMasterControllercs   {
       
       
            private readonly IProdcutMasterAppServicecs _ProdcutMasterAppServicecs;

            public ProductMasterControllercs(IProdcutMasterAppServicecs ProdcutMasterAppServicecs)
            {
                _ProdcutMasterAppServicecs = ProdcutMasterAppServicecs;
            }

            [HttpPost]
            [Route("CreateOrUpdateProduct")]
            public virtual Task CreateOrUpdateProduct(ProductMasterDto input)
            {
                return _ProdcutMasterAppServicecs.CreateOrUpdateProduct(input);
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
