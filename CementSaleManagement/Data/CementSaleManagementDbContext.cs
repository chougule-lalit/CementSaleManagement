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
