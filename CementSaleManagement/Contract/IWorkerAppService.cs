using CementSaleManagement.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CementSaleManagement.Contract
{
    public interface IWorkerAppService
    {
        //wroker Master

        Task CreateOrUpdateWoker(WorkerMasterCreateUpdateDto input);

        //  Task DeleteWorkerAsync(int id);
    }
}
