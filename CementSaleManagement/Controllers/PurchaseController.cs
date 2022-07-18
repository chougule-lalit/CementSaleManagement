using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : Controller, IPurchaseAppService
    {
        private readonly IPurchaseAppService _purchaseAppService;

        public PurchaseController(IPurchaseAppService purchaseAppService)
        {
            _purchaseAppService = purchaseAppService;
        }

        [HttpPost]
        [Route("cancelPurchase")]
        public virtual Task<bool> CancelPurchase(int id)
        {
            return _purchaseAppService.CancelPurchase(id);
        }

        [HttpPost]
        [Route("createOrUpdate")]
        public virtual Task<bool> CreateOrUpdate(PurchaseMasterDto input)
        {
            return _purchaseAppService.CreateOrUpdate(input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task<bool> DeleteAsync(int id)
        {
            return _purchaseAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("fetchCancelledPurchaseList")]
        public virtual Task<PagedResultDto<PurchaseDto>> FetchCancelledPurchaseListAsync(GetPurchaseInputDto input)
        {
            return _purchaseAppService.FetchCancelledPurchaseListAsync(input);
        }

        [HttpPost]
        [Route("fetchPurchaseList")]
        public virtual Task<PagedResultDto<PurchaseDto>> FetchPurchaseListAsync(GetPurchaseInputDto input)
        {
            return _purchaseAppService.FetchPurchaseListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<PurchaseMasterDto> GetAsync(int id)
        {
            return _purchaseAppService.GetAsync(id);
        }

        [NonAction]
        public Task<ExportToExcelDto> DownloadReportAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getPurchaseReport")]
        public async Task<IActionResult> GetPurchaseReportAsync()
        {
            var fileDto = await _purchaseAppService.DownloadReportAsync();
            return File(fileDto.Content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDto.Name);
        }

        [NonAction]
        public Task<ExportToExcelDto> DownloadCancelReportAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getPurchaseCancelReport")]
        public async Task<IActionResult> GetPurchaseCancelReportAsync()
        {
            var fileDto = await _purchaseAppService.DownloadCancelReportAsync();
            return File(fileDto.Content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDto.Name);
        }
    }
}
