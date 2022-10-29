using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<DataAccess.Entities.Parcel> Parcels { get; set; }
        
        public DbSet<DataAccess.Entities.Warehouse> Warehouses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=swkom-sqlserver-1\\SQLEXPRESS;" +
                "DataBase=SWKOMDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessLogic.Entities.Parcel>()
                .ToTable("parcels");
        }

    }

}


