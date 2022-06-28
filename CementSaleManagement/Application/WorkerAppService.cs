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
    public class WorkerAppService : IWorkerAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        public async Task CreateOrUpdateWoker(WorkerMasterCreateUpdateDto input)
        {
            if (input.Id.HasValue)
            {
                var worker = await _dbContext.WorkerMaster.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (worker != null)
                {
                    _mapper.Map(input, worker);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var workerToCreate = _mapper.Map<WorkerMaster>(input);
                await _dbContext.AddAsync(workerToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
