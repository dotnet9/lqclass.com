﻿// <auto-generated />
using System;
using LQClass.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LQClass.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210316055913_DataSeeding")]
    partial class DataSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-preview.2.21154.2");

            modelBuilder.Entity("LQClass.Api.Models.TouristRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("TEXT");

                    b.Property<double?>("DiscountPresent")
                        .HasColumnType("REAL");

                    b.Property<string>("Features")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fees")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TouristRoutes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0b227792-5d63-4b54-ad4d-a4a7613650cb"),
                            CreateTime = new DateTime(2021, 3, 16, 5, 59, 12, 910, DateTimeKind.Utc).AddTicks(5543),
                            Description = "说明",
                            OriginalPrice = 0m,
                            Title = "测试标题"
                        });
                });

            modelBuilder.Entity("LQClass.Api.Models.TouristRoutePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TouristRouteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TouristRouteId");

                    b.ToTable("TouristRoutePictures");
                });

            modelBuilder.Entity("LQClass.Api.Models.TouristRoutePicture", b =>
                {
                    b.HasOne("LQClass.Api.Models.TouristRoute", "TouristRoute")
                        .WithMany("TouristRoutePictures")
                        .HasForeignKey("TouristRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TouristRoute");
                });

            modelBuilder.Entity("LQClass.Api.Models.TouristRoute", b =>
                {
                    b.Navigation("TouristRoutePictures");
                });
#pragma warning restore 612, 618
        }
    }
}