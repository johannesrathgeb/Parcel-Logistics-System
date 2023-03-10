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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;    
 

namespace LRLogistik.LRPackage.DataAccess.Sql
{
    [ExcludeFromCodeCoverage]
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options) 
        {
        }

        public virtual DbSet<DataAccess.Entities.Parcel> Parcels { get; set; }
        
        public virtual DbSet<DataAccess.Entities.Hop> Hops { get; set; }

        public virtual DbSet<WebhookResponse> WebhookResponse { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

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

            modelBuilder.Entity<DataAccess.Entities.Warehouse>(e =>
            {
                e.HasMany<WarehouseNextHops>(p => p.NextHops);
            });

            modelBuilder.Entity<Hop>()
                          .HasDiscriminator()
                          .HasValue<Warehouse>("Level")
                          .HasValue<Transferwarehouse>("Region")
                          .HasValue<Transferwarehouse>("LogisticPartner")
                          .HasValue<Transferwarehouse>("LogisticPartnerUrl")
                          .HasValue<Truck>("Region")
                          .HasValue<Truck>("NumberPlate");

            //modelBuilder.Entity<Hop>(e =>
            //{
            //    e.HasKey(c => c.HopId);
            //    e.Property(c => c.HopId).ValueGeneratedOnAdd();
            //    e.Property(c => c.LocationCoordinates).HasColumnType("geometry");

            //    e.HasDiscriminator<string>("HopType")
            //        .HasValue<Warehouse>("Level")
            //        .HasValue<Transferwarehouse>("Region")
            //        .HasValue<Transferwarehouse>("LogisticsPartner")
            //        .HasValue<Transferwarehouse>("LogisticsPartnerUrl")
            //        .HasValue<Truck>("Region")
            //        .HasValue<Truck>("PlateNumber");

            //});


            modelBuilder.Entity<Hop>(e =>
            {
                e.Property(x => x.HopId).ValueGeneratedOnAdd();
                e.Property(h => h.LocationCoordinates).HasColumnType("geometry");
            });

            modelBuilder.Entity<HopArrival>(e =>
            {
                e.Property(x => x.HopArrivalId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Transferwarehouse>()
                .Property(t => t.Region).HasColumnType("geometry");

            modelBuilder.Entity<Truck>()
                .Property(t => t.Region).HasColumnType("geometry");

            modelBuilder.Entity<WarehouseNextHops>(e =>
            {
                e.HasKey(w => w.WarehouseNextHopsId);
                e.Property(w => w.WarehouseNextHopsId).ValueGeneratedOnAdd();
                e.Property(w => w.TraveltimeMins).IsRequired();
                e.HasOne<Hop>(w => w.Hop);
            });

            modelBuilder.Entity<WebhookResponse>(e =>
            {
                e.HasKey(webhook => webhook.Id);
                e.Property(webhook => webhook.Id).ValueGeneratedOnAdd();
            });
        }
    }
}


