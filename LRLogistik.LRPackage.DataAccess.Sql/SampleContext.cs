using LRLogistik.LRPackage.DataAccess.Entities;
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

        public DbSet<DataAccess.Entities.Parcel> Parcels { get; set; }
        
        //public DbSet<BusinessLogic.Entities.Warehouse> Warehouses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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


            modelBuilder.Entity<DataAccess.Entities.Recipient>(e =>
            {
                //e.ToTable("parcels");
                e.HasKey(x => x.RecipientId);
                e.Property(x => x.RecipientId).ValueGeneratedOnAdd(); 
            });

            modelBuilder.Entity<DataAccess.Entities.Parcel>(e =>
            {
                //e.ToTable("parcels");
                e.Property(p => p.ParcelId).ValueGeneratedOnAdd(); 
                e.HasKey(p => p.ParcelId);
                e.HasOne<Recipient>(p => p.Recipient);
                e.HasOne<Recipient>(p => p.Sender);
                e.HasMany<HopArrival>(p => p.VisitedHops); 
                e.HasMany<HopArrival>(p => p.FutureHops);
            });



        }
    }
}


