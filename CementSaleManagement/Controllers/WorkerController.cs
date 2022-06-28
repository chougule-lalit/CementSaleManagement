using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CementSaleManagement.Controllers
{
    public class WorkerController : IWorkerAppService
    {
        private readonly IWorkerAppService _workerappAppService;
        ///
        [HttpPost]
        [Route("CreateOrUpdateWoker")]
        public Task CreateOrUpdateWoker(WorkerMasterCreateUpdateDto input)
        {
            return _workerappAppService.CreateOrUpdateWoker(input);
        }

        //[HttpDelete]
        //[Route("DeleteWorkerAsync")]
        //public virtual Task DeleteWorkerAsync(int id)
        //{
        //    return _roleMasterAppService.DeleteWorkerAsync(id);
        //}

    }
}
