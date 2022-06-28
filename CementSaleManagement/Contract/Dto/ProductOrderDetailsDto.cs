using CementSaleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class ProductOrderDetailsDto
    {

		public int? Id { get; set; }
		public int SUPPLIER_ID { get; set; }
		public string ADDRESS { get; set; }

		public string MOBILE_NO { get; set; }

		public string RAW_MATERIAL_NAME { get; set; }

		public string QUANTITY { get; set; }

		public string RATE { get; set; }

		public int TAX { get; set; }
	}
}
