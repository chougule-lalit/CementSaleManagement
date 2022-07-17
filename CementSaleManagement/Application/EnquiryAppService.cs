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
    public class EnquiryAppService : IEnquiryAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public EnquiryAppService(CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task CreateOrUpdate(EnquiryDto input)
        {
            if (input.Id.HasValue)
            {
                var user = await _dbContext.Enquiries.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                if (user != null)
                {
                    _mapper.Map(input, user);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                var userToCreate = _mapper.Map<Enquiry>(input);
                await _dbContext.AddAsync(userToCreate);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<EnquiryDto> GetAsync(int id)
        {
            var data = await _dbContext.Enquiries.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            return _mapper.Map<EnquiryDto>(data);
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.Enquiries.FirstOrDefaultAsync(x => x.Id == id);

            if (data != null)
            {
                _dbContext.Enquiries.Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<EnquiryDto>> FetchEnquiryListAsync(GetEnquiryInputDto input)
        {
            var data = await _dbContext.Enquiries.ToListAsync();

            var count = data.Count;

            var returnData = data.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<EnquiryDto>
            {
                Items = _mapper.Map<List<EnquiryDto>>(returnData),
                TotalCount = count
            };
        }
    }
}
