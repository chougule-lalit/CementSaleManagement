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
    public class OrderAppService : IOrderAppService
    {
        private readonly CementSaleManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderAppService(
            CementSaleManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<bool> CancelOrder(int id)
        {
            try
            {

                var order = await _dbContext.OrderMasters.FirstOrDefaultAsync(x => x.Id == id);

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

        public virtual async Task<bool> CreateOrUpdate(OrderMasterDto input)
        {
            try
            {
                if (input.Id.HasValue)
                {
                    var orderMaster = await _dbContext.OrderMasters.FirstOrDefaultAsync(x => x.Id == input.Id.Value);
                    if (orderMaster != null)
                    {
                        //Calculating price
                        var productIds = input.OrderDetails.Select(x => x.ProductMasterId).Distinct().ToList();
                        var products = await _dbContext.ProductMasters.Where(x => productIds.Contains(x.Id)).ToListAsync();

                        decimal productPrice = 0;
                        int totalCount = 0;
                        foreach (var item in products)
                        {
                            var count = input.OrderDetails.Where(x => x.ProductMasterId == item.Id).Select(x => x.Count).FirstOrDefault();
                            totalCount = totalCount + count;
                            productPrice = productPrice + (count * item.Price);
                        }

                        input.Amount = productPrice;
                        input.ItemCount = totalCount;

                        _mapper.Map(input, orderMaster);
                        await _dbContext.SaveChangesAsync();


                        //Deleting old entries
                        var orderDetailToRemove = await _dbContext.OrderDetails.Where(x => x.OrderMasterId == orderMaster.Id).ToListAsync();
                        _dbContext.OrderDetails.RemoveRange(orderDetailToRemove);
                        await _dbContext.SaveChangesAsync();

                        //inserting again
                        foreach (var item in input.OrderDetails)
                        {
                            var orderDetail = _mapper.Map<OrderDetail>(item);
                            orderDetail.OrderMasterId = orderMaster.Id;
                            await _dbContext.OrderDetails.AddAsync(orderDetail);
                        }

                        await _dbContext.SaveChangesAsync();

                    }
                }
                else
                {
                    //Calculating price
                    var productIds = input.OrderDetails.Select(x => x.ProductMasterId).Distinct().ToList();
                    var products = await _dbContext.ProductMasters.Where(x => productIds.Contains(x.Id)).ToListAsync();

                    decimal productPrice = 0;
                    int totalCount = 0;
                    foreach (var item in products)
                    {
                        var count = input.OrderDetails.Where(x => x.ProductMasterId == item.Id).Select(x => x.Count).FirstOrDefault();
                        totalCount = totalCount + count;
                        productPrice = productPrice + (count * item.Price);
                    }

                    input.Amount = productPrice;
                    input.OrderDate = DateTime.Now;
                    input.ItemCount = totalCount;
                    input.IsActive = true;

                    var orderMaster = _mapper.Map<OrderMaster>(input);
                    await _dbContext.OrderMasters.AddAsync(orderMaster);
                    await _dbContext.SaveChangesAsync();

                    foreach (var item in input.OrderDetails)
                    {
                        var orderDetail = _mapper.Map<OrderDetail>(item);
                        orderDetail.OrderMasterId = orderMaster.Id;
                        await _dbContext.OrderDetails.AddAsync(orderDetail);
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
                var data = await _dbContext.OrderMasters.FirstOrDefaultAsync(x => x.Id == id);

                if (data != null)
                {
                    _dbContext.OrderMasters.Remove(data);
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

        public virtual async Task<PagedResultDto<OrderDto>> FetchCancelledOrderListAsync(GetOrderInputDto input)
        {
            var data = from o in _dbContext.OrderMasters
                       join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                       where !o.IsActive
                       select new OrderDto
                       {
                           Amount = o.Amount,
                           CustomerName = $"{c.FirstName} {c.LastName}",
                           IsActive = o.IsActive,
                           ItemCount = o.ItemCount,
                           OrderDate = o.OrderDate,
                           OrderId = o.Id,
                           CancelDate = o.CancelDate
                       };

            if (input.OrderDate.HasValue)
            {
                data = data.Where(x => x.OrderDate.Date == input.OrderDate.Value.Date);
            }

            var dataList = await data.ToListAsync();

            var count = dataList.Count;

            var returnData = dataList.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<OrderDto>
            {
                Items = _mapper.Map<List<OrderDto>>(returnData),
                TotalCount = count
            };
        }

        public virtual async Task<PagedResultDto<OrderDto>> FetchOrderListAsync(GetOrderInputDto input)
        {
            var data = from o in _dbContext.OrderMasters
                       join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                       where o.IsActive
                       select new OrderDto
                       {
                           Amount = o.Amount,
                           CustomerName = $"{c.FirstName} {c.LastName}",
                           IsActive = o.IsActive,
                           ItemCount = o.ItemCount,
                           OrderDate = o.OrderDate,
                           OrderId = o.Id,
                           CancelDate = o.CancelDate
                       };

            if (input.OrderDate.HasValue)
            {
                data = data.Where(x => x.OrderDate.Date == input.OrderDate.Value.Date);
            }

            var dataList = await data.ToListAsync();

            var count = dataList.Count;

            var returnData = dataList.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<OrderDto>
            {
                Items = _mapper.Map<List<OrderDto>>(returnData),
                TotalCount = count
            };
        }

        public virtual async Task<OrderMasterDto> GetAsync(int id)
        {
            var data = await _dbContext.OrderMasters.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
                return null;

            var dataDetails = await _dbContext.OrderDetails.Where(x => x.OrderMasterId == id).ToListAsync();
            var mappedDetails = new List<OrderDetailDto>();

            if (dataDetails.Count > 0)
            {
                mappedDetails = _mapper.Map<List<OrderDetailDto>>(dataDetails);
            }

            var output = _mapper.Map<OrderMasterDto>(data);
            output.OrderDetails = mappedDetails;

            return output;
        }

        public virtual async Task<ExportToExcelDto> DownloadReportAsync()
        {
            var output = new ExportToExcelDto();
            var data = await (from o in _dbContext.OrderMasters
                              join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                              where o.IsActive
                              select new OrderDto
                              {
                                  Amount = o.Amount,
                                  CustomerName = $"{c.FirstName} {c.LastName}",
                                  IsActive = o.IsActive,
                                  ItemCount = o.ItemCount,
                                  OrderDate = o.OrderDate,
                                  OrderId = o.Id,
                                  CancelDate = o.CancelDate
                              }).ToListAsync();

            var currentRow = 0;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Purchase");

                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Order Id";
                worksheet.Cell(currentRow, 2).Value = "Customer Name";
                worksheet.Cell(currentRow, 3).Value = "Item Count";
                worksheet.Cell(currentRow, 4).Value = "Total Price";
                worksheet.Cell(currentRow, 5).Value = "Order Date";

                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.OutsideBorderColor = XLColor.Black;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range(currentRow, 1, currentRow, 5).Style.Border.InsideBorderColor = XLColor.Black;

                foreach (var item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).SetValue(item.OrderId);
                    worksheet.Cell(currentRow, 2).SetValue(item.CustomerName);
                    worksheet.Cell(currentRow, 3).SetValue(item.ItemCount);
                    worksheet.Cell(currentRow, 4).SetValue(item.Amount);
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.SetFormat("#,##0.00");
                    worksheet.Cell(currentRow, 5).SetValue(item.OrderDate.ToShortDateString());
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    output.Name = $"Order_Report.xlsx";
                    output.Content = stream.ToArray();
                }
            }

            return output;
        }

        public virtual async Task<ExportToExcelDto> DownloadCancelReportAsync()
        {
            var output = new ExportToExcelDto();
            var data = await (from o in _dbContext.OrderMasters
                              join c in _dbContext.UserMasters on o.UserMasterId equals c.Id
                              where !o.IsActive
                              select new OrderDto
                              {
                                  Amount = o.Amount,
                                  CustomerName = $"{c.FirstName} {c.LastName}",
                                  IsActive = o.IsActive,
                                  ItemCount = o.ItemCount,
                                  OrderDate = o.OrderDate,
                                  OrderId = o.Id,
                                  CancelDate = o.CancelDate
                              }).ToListAsync();

            var currentRow = 0;

            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Purchase");

                currentRow += 2;
                worksheet.Cell(currentRow, 1).Value = "Order Id";
                worksheet.Cell(currentRow, 2).Value = "Customer Name";
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
                    worksheet.Cell(currentRow, 1).SetValue(item.OrderId);
                    worksheet.Cell(currentRow, 2).SetValue(item.CustomerName);
                    worksheet.Cell(currentRow, 3).SetValue(item.ItemCount);
                    worksheet.Cell(currentRow, 4).SetValue(item.Amount);
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.SetFormat("#,##0.00");
                    worksheet.Cell(currentRow, 5).SetValue(item.CancelDate.ToShortDateString());
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    output.Name = $"Order_Cancel_Report.xlsx";
                    output.Content = stream.ToArray();
                }
            }

            return output;
        }
    }
}
