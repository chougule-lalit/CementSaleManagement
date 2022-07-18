using AutoMapper;
using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using CementSaleManagement.Data;
using CementSaleManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Application
{
    public class ProductMasterAppService : IProductMasterAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductMasterAppService(CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdate(ProductMasterDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.ProductMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<ProductMaster>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ProductMasterDto> GetAsync(int id)
        {
            var data = await _dbContext.ProductMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<ProductMasterDto>(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.ProductMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.ProductMasters.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<ProductMasterDto>> FetchProductMasterListAsync(GetProductMasterInputDto input)
        {
            var data = await _dbContext.ProductMasters.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<ProductMasterDto>
            {
                Items = _mapper.Map<List<ProductMasterDto>>(returnData),
                TotalCount = count
            };
        }

        public async Task<List<ProductMasterDropdownDto>> GetProductMasterDropdownAsync()
        {
            return await _dbContext.ProductMasters.Select(x => new ProductMasterDropdownDto
            {
                Id = x.Id,
                Name = $"{x.ProductName}",
                Price = x.Price
            }).ToListAsync();
        }
    }
}
