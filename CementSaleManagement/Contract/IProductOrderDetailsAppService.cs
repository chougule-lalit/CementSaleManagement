using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IProductOrderDetailsAppService
    {

    //    Task CreateOrUpdatePurchaseOrder(ProductOrderDetailsDto input);
         
        Task createpurchase(ProductOrderDetailsDto input);

    }
}
