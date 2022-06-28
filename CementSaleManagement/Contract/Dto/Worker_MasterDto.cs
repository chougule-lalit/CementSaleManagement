using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Contract.Dto
{
    public class Worker_MasterDto
    {
        public int? Id { get; set; }
        public string First_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Last_Name { get; set; }
        public string Joining_date { get; set; }
        public string DOB { get; set; }
        public string Experiance { get; set; }
        public string Phone { get; set; }
    }
    public class WorkerMasterCreateUpdateDto
    {
        public int? Id { get; set; }
        public string First_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Last_Name { get; set; }
        public string Joining_date { get; set; }
        public string DOB { get; set; }
        public string Experiance { get; set; }
        public string Phone { get; set; }


    }
}
 
