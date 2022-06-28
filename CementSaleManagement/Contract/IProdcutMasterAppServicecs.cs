using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract
{
    public interface IProdcutMasterAppServicecs
    {

        Task CreateOrUpdateProduct(ProductMasterDto input);
        

    }
}
