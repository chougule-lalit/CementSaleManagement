using AutoMapper;
using CementSaleManagement.Contract;
using CementSaleManagement.Contract.Dto;
using CementSaleManagement.Data;
using CementSaleManagement.Entities;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Application
{
    public class PurchaseAppService : IPurchaseAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public PurchaseAppService(
            CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<bool> CancelPurchase(int id)
        {
            try
            {

                var order = await _dbContext.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == id);

                if (order != null)
                {
                    order.IsActive = false;
                    order.CancelDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> CreateOrUpdate(PurchaseMasterDto input)
        {
            try
            {
                if (input.Id.HasValue)
                {
                    var purchaseMaster = await _dbContext.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                    if (purchaseMaster != null)
                    {
                        //Calculating price
                        var productIds = input.PurchaseDetails.Select(x => x.ProductMasterId).Distinct().ToList();
                        var products = await _dbContext.ProductMasters.Where(x => productIds.Contains(x.Id)).ToListAsync();

                        decimal productPrice = 0;
                        int totalCount = 0;
                        foreach (var item in products)
                        {
                            var count = input.PurchaseDetails.Where(x => x.ProductMasterId == item.Id).Select(x => x.Count).FirstOrDefault();
                            totalCount = totalCount + count;
                            productPrice = productPrice + (count * item.Price);
                        }

                        input.Amount = productPrice;
                        input.ItemCount = totalCount;

                        _mapper.Map(input, purchaseMaster);
                        await _dbContext.SaveChangesAsync();

                        //Deleting old entries
                        var orderDetailToRemove = await _dbContext.PurchaseDetails.Where(x => x.PurchaseMasterId == purchaseMaster.Id).ToListAsync();
                        _dbContext.PurchaseDetails.RemoveRange(orderDetailToRemove);
                        await _dbContext.SaveChangesAsync();

                        //inserting again
                        foreach (var item in input.PurchaseDetails)
                        {
                            var orderDetail = _mapper.Map<PurchaseDetail>(item);
                            orderDetail.PurchaseMasterId = purchaseMaster.Id;
                            await _dbContext.PurchaseDetails.AddAsync(orderDetail);
                        }

                        await _dbContext.SaveChangesAsync();

                    }
                }
                else
                {
                    //Calculating price
                    var productIds = input.PurchaseDetails.Select(x => x.ProductMasterId).Distinct().ToList();
                    var products = await _dbContext.ProductMasters.Where(x => productIds.Contains(x.Id)).ToListAsync();

                    decimal productPrice = 0;
                    int totalCount = 0;
                    foreach (var item in products)
                    {
                        var count = input.PurchaseDetails.Where(x => x.ProductMasterId == item.Id).Select(x => x.Count).FirstOrDefault();
                        totalCount = totalCount + count;
                        productPrice = productPrice + (count * item.Price);
                    }

                    input.Amount = productPrice;
                    input.PurchaseDate = DateTime.Now;
                    input.ItemCount = totalCount;
                    input.IsActive = true;

                    var purchaseMaster = _mapper.Map<PurchaseMaster>(input);
                    await _dbContext.PurchaseMasters.AddAsync(purchaseMaster);
                    await _dbContext.SaveChangesAsync();

                    foreach (var item in input.PurchaseDetails)
                    {
                        var orderDetail = _mapper.Map<PurchaseDetail>(item);
                        orderDetail.PurchaseMasterId = purchaseMaster.Id;
                        await _dbContext.PurchaseDetails.AddAsync(orderDetail);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var data = await _dbContext.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == id);

                if (data != null)
                {
                    _dbContext.PurchaseMasters.Remove(data);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public virtual async Task<PagedResultDto<PurchaseDto>> FetchCancelledPurchaseListAsync(GetPurchaseInputDto input)
        {
            var data = from o in _dbContext.PurchaseMasters
                       join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                       where !o.IsActive
                       select new PurchaseDto
                       {
                           Amount = o.Amount,
                           SupplierName = $"{c.FirstName} {c.LastName}",
                           IsActive = o.IsActive,
                           ItemCount = o.ItemCount,
                           PurchaseDate = o.PurchaseDate,
                           PurchaseId = o.Id,
                           CancelDate = o.CancelDate
                       };

            if (input.PurchaseDate.HasValue)
            {
                data = data.Where(x => x.PurchaseDate.Date == input.PurchaseDate.Value.Date);
            }

            var dataList = await data.ToListAsync();

            var count = dataList.Count;

            var returnData = dataList.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<PurchaseDto>
            {
                Items = _mapper.Map<List<PurchaseDto>>(returnData),
                TotalCount = count
            };
        }

        public virtual async Task<PagedResultDto<PurchaseDto>> FetchPurchaseListAsync(GetPurchaseInputDto input)
        {
            var data = from o in _dbContext.PurchaseMasters
                       join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                       where o.IsActive
                       select new PurchaseDto
                       {
                           Amount = o.Amount,
                           SupplierName = $"{c.FirstName} {c.LastName}",
                           IsActive = o.IsActive,
                           ItemCount = o.ItemCount,
                           PurchaseDate = o.PurchaseDate,
                           PurchaseId = o.Id,
                           CancelDate = o.CancelDate
                       };

            if (input.PurchaseDate.HasValue)
            {
                data = data.Where(x => x.PurchaseDate.Date == input.PurchaseDate.Value.Date);
            }

            var dataList = await data.ToListAsync();

            var count = dataList.Count;

            var returnData = dataList.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<PurchaseDto>
            {
                Items = _mapper.Map<List<PurchaseDto>>(returnData),
                TotalCount = count
            };
        }

        public virtual async Task<PurchaseMasterDto> GetAsync(int id)
        {
            var data = await _dbContext.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            var dataDetails = await _dbContext.PurchaseDetails.Where(x => x.PurchaseMasterId == id).ToListAsync();
            var mappedDetails = new List<PurchaseDetailDto>();

            if (dataDetails.Count > 0)
            {
                mappedDetails = _mapper.Map<List<PurchaseDetailDto>>(dataDetails);
            }

            var output = _mapper.Map<PurchaseMasterDto>(data);
            output.PurchaseDetails = mappedDetails;

            return output;
        }

        public virtual async Task<ExportToExcelDto> DownloadReportAsync()
        {
            var output = new ExportToExcelDto();
            var data = await (from o in _dbContext.PurchaseMasters
                              join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                              where o.IsActive
                              select new PurchaseDto
                              {
                                  Amount = o.Amount,
                                  SupplierName = $"{c.FirstName} {c.LastName}",
                                  IsActive = o.IsActive,
                                  ItemCount = o.ItemCount,
                                  PurchaseDate = o.PurchaseDate,
                                  PurchaseId = o.Id
                              }).ToListAsync();

            var currentRow = 0;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Purchase");

                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Purchase Id";
                worksheet.Cell(currentRow, 2).Value = "Supplier Name";
                worksheet.Cell(currentRow, 3).Value = "Item Count";
                worksheet.Cell(currentRow, 4).Value = "Total Price";
                worksheet.Cell(currentRow, 5).Value = "Purchase Date";

                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorderColor = XLColor.Black;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorderColor = XLColor.Black;

                foreach (var item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).SetValue(item.PurchaseId);
                    worksheet.Cell(currentRow, 2).SetValue(item.SupplierName);
                    worksheet.Cell(currentRow, 3).SetValue(item.ItemCount);
                    worksheet.Cell(currentRow, 4).SetValue(item.Amount);
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.SetFormat("#,##0.00");
                    worksheet.Cell(currentRow, 5).SetValue(item.PurchaseDate.ToShortDateString());
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    output.Name = $"Purchase_Report.xlsx";
                    output.Content = stream.ToArray();
                }
            }

            return output;
        }

        public virtual async Task<ExportToExcelDto> DownloadCancelReportAsync()
        {
            var output = new ExportToExcelDto();
            var data = await (from o in _dbContext.PurchaseMasters
                              join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                              where !o.IsActive
                              select new PurchaseDto
                              {
                                  Amount = o.Amount,
                                  SupplierName = $"{c.FirstName} {c.LastName}",
                                  IsActive = o.IsActive,
                                  ItemCount = o.ItemCount,
                                  CancelDate = o.PurchaseDate,
                                  PurchaseId = o.Id
                              }).ToListAsync();

            var currentRow = 0;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Purchase");

                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Purchase Id";
                worksheet.Cell(currentRow, 2).Value = "Supplier Name";
                worksheet.Cell(currentRow, 3).Value = "Item Count";
                worksheet.Cell(currentRow, 4).Value = "Total Price";
                worksheet.Cell(currentRow, 5).Value = "Cancel Date";

                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorderColor = XLColor.Black;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorderColor = XLColor.Black;

                foreach (var item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).SetValue(item.PurchaseId);
                    worksheet.Cell(currentRow, 2).SetValue(item.SupplierName);
                    worksheet.Cell(currentRow, 3).SetValue(item.ItemCount);
                    worksheet.Cell(currentRow, 4).SetValue(item.Amount);
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.SetFormat("#,##0.00");
                    worksheet.Cell(currentRow, 5).SetValue(item.CancelDate.ToShortDateString());
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    output.Name = $"Purchase_Cancel_Report.xlsx";
                    output.Content = stream.ToArray();
                }
            }

            return output;
        }
    }
}
