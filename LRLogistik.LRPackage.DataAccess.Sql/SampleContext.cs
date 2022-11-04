using LRLogistik.LRPackage.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;    
 

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options) 
        {
            this.Database.EnsureCreated();
        }

        public SampleContext() 
        {
            this.Database.EnsureCreated();
        }

        public DbSet<BusinessLogic.Entities.Parcel> Parcels { get; set; }
        
        //public DbSet<BusinessLogic.Entities.Warehouse> Warehouses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(
            //    "Server=swkom-sqlserver-1\\SQLEXPRESS;" +
            //    "DataBase=SWKOMDB;Trusted_Connection=True;");
            //var dbUser = "sa";
            //var dbPW = "pass@word1";
            //optionsBuilder.UseSqlServer(
            //    $"Server=tcp:sample-sql.database.windows.net,1433;Initial Catalog=sample-db;Persist Security Info=False;User ID={dbUser};Password={dbPW};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("SWKOMDB");
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessLogic.Entities.Parcel>()
                .ToTable("parcels");
        }
    }

}


