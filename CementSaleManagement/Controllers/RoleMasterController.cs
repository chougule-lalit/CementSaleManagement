using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CementSaleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleMasterController : IRoleMasterAppService
    {
        private readonly IRoleMasterAppService _roleMasterAppService;

        public RoleMasterController(IRoleMasterAppService roleMasterAppService)
        {
            _roleMasterAppService = roleMasterAppService;
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task CreateOrUpdateAsync(RoleMasterDto input)
        {
            return _roleMasterAppService.CreateOrUpdateAsync(input);
        }

        [HttpDelete]
        [Route("delete")]
        public virtual Task DeleteRoleAsync(int id)
        {
            return _roleMasterAppService.DeleteRoleAsync(id);
        }

        [HttpGet]
        [Route("getRole")]
        public virtual Task<RoleMasterDto> GetRoleAsync(int id)
        {
            return _roleMasterAppService.GetRoleAsync(id);
        }
    }
}
