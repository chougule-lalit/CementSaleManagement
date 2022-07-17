﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Entities
{
    public class SupplierMaster: BaseEntity
    {
        public string SupplierName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }
        
        public string Email { get; set; }

        public string Phone { get; set; }

        public UserMaster UserMaster { get; set; }

        public int UserMasterId { get; set; }
    }
}
