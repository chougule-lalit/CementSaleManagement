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
    public class SupplierMasterAppService : ISupplierMasterAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public SupplierMasterAppService(CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdate(SupplierMasterDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.SupplierMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<SupplierMaster>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<SupplierMasterDto> GetAsync(int id)
        {
            var data = await _dbContext.SupplierMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<SupplierMasterDto>(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.SupplierMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.SupplierMasters.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<SupplierMasterDto>> FetchSupplierMasterListAsync(GetSupplierMasterInputDto input)
        {
            var data = await _dbContext.SupplierMasters.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<SupplierMasterDto>
            {
                Items = _mapper.Map<List<SupplierMasterDto>>(returnData),
                TotalCount = count
            };
        }

        public async Task<List<SupplierMasterDropdownDto>> GetSupplierMasterDropdownAsync()
        {
            return await _dbContext.SupplierMasters.Select(x => new SupplierMasterDropdownDto
            {
                Id = x.Id,
                Name = x.SupplierName
            }).ToListAsync();
        }
    }
}
