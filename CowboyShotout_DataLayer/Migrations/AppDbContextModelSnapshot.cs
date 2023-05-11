﻿// <auto-generated />
using System;
using CowboyShotout_DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CowboyShotout_DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CowboyShotout_DataLayer.Models.Dbo.CowboyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ChangedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ChangedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GunId")
                        .HasColumnType("int");

                    b.Property<string>("Hair")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Health")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("HitRate")
                        .HasColumnType("float");

                    b.Property<byte>("IsValid")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GunId");

                    b.ToTable("CowboyModels");
                });

            modelBuilder.Entity("CowboyShotout_DataLayer.Models.Dbo.GunModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BulletsLeft")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ChangedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GunName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("IsValid")
                        .HasColumnType("tinyint");

                    b.Property<int>("MaxBullets")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GunModel");
                });

            modelBuilder.Entity("CowboyShotout_DataLayer.Models.Dbo.Position", b =>
                {
                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.Property<double>("Z")
                        .HasColumnType("float");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("CowboyShotout_DataLayer.Models.Dbo.CowboyModel", b =>
                {
                    b.HasOne("CowboyShotout_DataLayer.Models.Dbo.GunModel", "Gun")
                        .WithMany()
                        .HasForeignKey("GunId");

                    b.Navigation("Gun");
                });
#pragma warning restore 612, 618
        }
    }
}