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
    public class CustomerMasterAppService : ICustomerMasterAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerMasterAppService(CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdate(CustomerMasterDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.CustomerMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<CustomerMaster>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CustomerMasterDto> GetAsync(int id)
        {
            var data = await _dbContext.CustomerMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<CustomerMasterDto>(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.CustomerMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.CustomerMasters.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<CustomerMasterDto>> FetchCustomerMasterListAsync(GetCustomerMasterInputDto input)
        {
            var data = await _dbContext.CustomerMasters.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<CustomerMasterDto>
            {
                Items = _mapper.Map<List<CustomerMasterDto>>(returnData),
                TotalCount = count
            };
        }

        public async Task<List<CustomerMasterDropdownDto>> GetCustomerMasterDropdownAsync()
        {
            return await _dbContext.CustomerMasters.Select(x => new CustomerMasterDropdownDto
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}"
            }).ToListAsync();
        }
    }
}
