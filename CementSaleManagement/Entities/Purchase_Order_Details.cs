using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class Purchase_Order_Details
	{
       

        public int Id { get; set; }
		public Supplier_master SUPPLIER_ID { get; set; }
		public string ADDRESS { get; set; }

		public string MOBILE_NO { get; set; }

		public string RAW_MATERIAL_NAME { get; set; }

		public string QUANTITY { get; set; }

		public string RATE { get; set; }

		public int TAX { get; set; }

	}
}
