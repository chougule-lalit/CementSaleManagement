using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class ExportToExcelDto
    {
        public string Name { get; set; }

        public byte[] Content { get; set; }
    }
}
