using Microsoft.EntityFrameworkCore;
using CementSaleManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CementSaleManagement.Data
{
    public class CementSaleManagementDbContext : DbContext
    {
        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<RoleMaster> RoleMasters { get; set; }
        public DbSet<Supplier_master> Supplier_master { get; set; }

        public DbSet<ProductMaster> ProductMaster { get; set; }

        public DbSet<WorkerMaster> WorkerMaster { get; set; }

        public DbSet<Purchase_Order_Details> Purchase_Order_Details { get; set; }
        

        public CementSaleManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMaster>()
                .HasOne(s => s.Role)
                .WithMany().HasForeignKey(x => x.RoleId);
        }
    }
}
