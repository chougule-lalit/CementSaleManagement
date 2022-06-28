using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using CementSaleManagement.Data;
using CementSaleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Application
{
    public class ProductAppService : IProdcutMasterAppServicecs
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductAppService(
            CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task CreateOrUpdateProduct(ProductMasterDto input)
        {
            if (input.Id.HasValue)
            {
                var product = await _dbContext.ProductMaster.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (product != null)
                {
                    _mapper.Map(input, product);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var productToCreate = _mapper.Map<ProductMaster>(input);
                await _dbContext.AddAsync(productToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        //public async Task<PagedResultDto<Supplier_Masterdto>> FetchSuplierListAsync(GetSupplierInput input)
        //{
        //    var data = from u in _dbContext.Supplier_master
        //              // join r in _dbContext.Supplier_master on u.RoleId equals r.Id
        //               select new Supplier_Masterdto
        //               {
        //                   Email = u.Email,
        //                   Supplier_Name = u.Supplier_Name,
        //                   Supplier_Address = u.Supplier_Address,
        //                   Product_Name=u.Product_Name,
        //                   Id = u.Id,
        //                   Phone = u.Phone,

        //               };

        //    if (!string.IsNullOrEmpty(input.Search))
        //        data = data.Where(x => x.Supplier_Name.ToLower().Contains(input.Search.ToLower()));

        //    var count = data.Count();

        //    var userList = await data.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

        //    return new PagedResultDto<Supplier_Masterdto>
        //    {
        //        Items = userList,
        //        TotalCount = count
        //    };
        //}

        //public async Task<Supplier_Masterdto> GetSupplierAsync(int id)
        //{
        //    var user = await _dbContext.Supplier_master.FirstOrDefaultAsync(x => x.Id == id);

        //    if (user == null)
        //        return null;

        //    return _mapper.Map<Supplier_Masterdto>(user);
        //}

        //public async Task DeletesupplierAsync(int id)
        //{
        //    var user = await _dbContext.Supplier_master.FirstOrDefaultAsync(x => x.Id == id);

        //    if (user != null)
        //    {
        //        _dbContext.Supplier_master.Remove(user);
        //    }
        //}

    }
}
