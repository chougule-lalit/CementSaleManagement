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
    public class RoleMasterAppService: IRoleMasterAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleMasterAppService(
            CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateAsync(RoleMasterDto input)
        {
            if (input.Id.HasValue)
            {
                var role = await _dbContext.RoleMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (role != null)
                {
                    _mapper.Map(input, role);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var roleToCreate = _mapper.Map<RoleMaster>(input);
                await _dbContext.AddAsync(roleToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<RoleMasterDto> GetRoleAsync(int id)
        {
            var user = await _dbContext.RoleMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return _mapper.Map<RoleMasterDto>(user);
        }

        public async Task DeleteRoleAsync(int id)
        {
            var user = await _dbContext.RoleMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                _dbContext.RoleMasters.Remove(user);
            }
        }
    }
}
