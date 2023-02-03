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
    [Migration("20210119113037_stockOrder-beer-link")]
    partial class stockOrderbeerlink
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

                    b.Property<string>("BreweryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCider")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Strength")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beers");
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

                    b.Property<string>("UnitSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.ToTable("StockOrders");
                });

            modelBuilder.Entity("MonksInn.Domain.StockOrder", b =>
                {
                    b.HasOne("MonksInn.Domain.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId");

                    b.Navigation("Beer");
                });
#pragma warning restore 612, 618
        }
    }
}
