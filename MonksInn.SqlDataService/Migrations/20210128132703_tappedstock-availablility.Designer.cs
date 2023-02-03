﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonksInn.SqlDataService;

namespace MonksInn.SqlDataService.Migrations
{
    [DbContext(typeof(SqlDataContext))]
    [Migration("20210128132703_tappedstock-availablility")]
    partial class tappedstockavailablility
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("MonksInn.Domain.Beer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BeerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BeerType")
                        .HasColumnType("int");

                    b.Property<string>("BreweryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Strength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SubType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("MonksInn.Domain.CellarStockItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ArchiveReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SellByDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("WholesalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.ToTable("CellarStockItems");
                });

            modelBuilder.Entity("MonksInn.Domain.PubLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("DefaultIsDeliveryLocation")
                        .HasColumnType("bit");

                    b.Property<bool>("DefaultIsTakeawayLocation")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PubLocations");
                });

            modelBuilder.Entity("MonksInn.Domain.StockOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CreateWeeklyOrder")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ETA")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RecievedBeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("RecievedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RecievedInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("RecievedUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RecievedUnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecievedUnits")
                        .HasColumnType("int");

                    b.Property<string>("UnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("RecievedBeerId");

                    b.ToTable("StockOrders");
                });

            modelBuilder.Entity("MonksInn.Domain.StoreDefaultPricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWholesalePricing")
                        .HasColumnType("bit");

                    b.Property<decimal?>("RetailAbvLimit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RetailBeerType")
                        .HasColumnType("int");

                    b.Property<decimal?>("RetailDefaultAbvPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("WholeSaleDefaultFlatMarkup")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("WholeSaleUnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StoreDefaultPricings");
                });

            modelBuilder.Entity("MonksInn.Domain.StoreUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWholeSaleUser")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StoreUsers");
                });

            modelBuilder.Entity("MonksInn.Domain.SystemUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Salt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("MonksInn.Domain.TappedStockItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ForDelivery")
                        .HasColumnType("bit");

                    b.Property<bool>("ForTakeaway")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PubLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("RetailPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TapType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("PubLocationId");

                    b.ToTable("TappedStockItems");
                });

            modelBuilder.Entity("MonksInn.Domain.CellarStockItem", b =>
                {
                    b.HasOne("MonksInn.Domain.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");
                });

            modelBuilder.Entity("MonksInn.Domain.StockOrder", b =>
                {
                    b.HasOne("MonksInn.Domain.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId");

                    b.HasOne("MonksInn.Domain.Beer", "RecievedBeer")
                        .WithMany()
                        .HasForeignKey("RecievedBeerId");

                    b.Navigation("Beer");

                    b.Navigation("RecievedBeer");
                });

            modelBuilder.Entity("MonksInn.Domain.TappedStockItem", b =>
                {
                    b.HasOne("MonksInn.Domain.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MonksInn.Domain.PubLocation", "PubLocation")
                        .WithMany("TappedStockItems")
                        .HasForeignKey("PubLocationId");

                    b.Navigation("Beer");

                    b.Navigation("PubLocation");
                });

            modelBuilder.Entity("MonksInn.Domain.PubLocation", b =>
                {
                    b.Navigation("TappedStockItems");
                });
#pragma warning restore 612, 618
        }
    }
}
