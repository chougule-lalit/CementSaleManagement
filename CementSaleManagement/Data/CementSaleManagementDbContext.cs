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
        public DbSet<SupplierMaster> SupplierMasters { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<WorkerMaster> WorkerMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PurchaseMaster> PurchaseMasters { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        public CementSaleManagementDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleMaster>().HasData(
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Admin), Name = RoleEnum.Admin.ToString() },
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Worker), Name = RoleEnum.Worker.ToString() },
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Supplier), Name = RoleEnum.Supplier.ToString() },
                   new RoleMaster { Id = Convert.ToInt32(RoleEnum.Customer), Name = RoleEnum.Customer.ToString() }
                   );

            modelBuilder.Entity<UserMaster>().HasData(
                new UserMaster { Id = 1, Email = "admin@admin.com", FirstName = "Admin", LastName = "Admin", Phone = "7898765467", RoleId = 1 }
                );

            modelBuilder.Entity<ProductMaster>().HasData(
                new ProductMaster { Id = 1, ProductName = "OnePlus-Nord", CompanyName = "OnePlus", Price = 22000},
                new ProductMaster { Id = 2, ProductName = "OnePlus-7 Pro", CompanyName = "OnePlus", Price = 51000 },
                new ProductMaster { Id = 3, ProductName = "OnePlus-10", CompanyName = "OnePlus", Price = 80000 }
                );

        }
    }
}
