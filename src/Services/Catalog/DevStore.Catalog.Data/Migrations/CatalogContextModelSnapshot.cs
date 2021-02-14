﻿// <auto-generated />
using System;
using DevStore.Catalog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevStore.Catalog.Data.Migrations
{
    [DbContext(typeof(CatalogContext))]
    partial class CatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevStore.Catalog.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f5cc95e-d840-4fb0-aea4-cfe051b436ec"),
                            Code = 1,
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = new Guid("5c53372d-113e-47ca-acaa-b1e34226aa22"),
                            Code = 2,
                            Name = "Category 2"
                        });
                });

            modelBuilder.Entity("DevStore.Catalog.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<int>("EnrollimentLimit")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalOfEnrolled")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa364074-93a0-4742-85b5-2aa996857c79"),
                            CategoryId = new Guid("1f5cc95e-d840-4fb0-aea4-cfe051b436ec"),
                            CreatedDate = new DateTime(2021, 2, 14, 19, 30, 9, 490, DateTimeKind.Local).AddTicks(8482),
                            Description = "Description 1",
                            Enable = true,
                            EnrollimentLimit = 10,
                            Image = "image1.jpg",
                            Name = "Course 1",
                            Price = 10m,
                            TotalOfEnrolled = 0,
                            Video = "video1.mp4"
                        },
                        new
                        {
                            Id = new Guid("e0580a65-4e31-44bc-8bb5-7391892a4d56"),
                            CategoryId = new Guid("5c53372d-113e-47ca-acaa-b1e34226aa22"),
                            CreatedDate = new DateTime(2021, 2, 14, 19, 30, 9, 492, DateTimeKind.Local).AddTicks(4075),
                            Description = "Description 2",
                            Enable = true,
                            EnrollimentLimit = 20,
                            Image = "image2.jpg",
                            Name = "Course 2",
                            Price = 20m,
                            TotalOfEnrolled = 0,
                            Video = "video1.mp4"
                        });
                });

            modelBuilder.Entity("DevStore.Catalog.Domain.Course", b =>
                {
                    b.HasOne("DevStore.Catalog.Domain.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DevStore.Catalog.Domain.Period", "Period", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("EndDate")
                                .HasColumnName("EndDate")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("StartDate")
                                .HasColumnName("StartDate")
                                .HasColumnType("datetime2");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");

                            b1.HasData(
                                new
                                {
                                    CourseId = new Guid("aa364074-93a0-4742-85b5-2aa996857c79"),
                                    EndDate = new DateTime(2021, 3, 14, 19, 30, 9, 488, DateTimeKind.Local).AddTicks(1846),
                                    StartDate = new DateTime(2021, 2, 14, 19, 30, 9, 484, DateTimeKind.Local).AddTicks(112)
                                },
                                new
                                {
                                    CourseId = new Guid("e0580a65-4e31-44bc-8bb5-7391892a4d56"),
                                    EndDate = new DateTime(2021, 4, 14, 19, 30, 9, 489, DateTimeKind.Local).AddTicks(7821),
                                    StartDate = new DateTime(2021, 2, 14, 19, 30, 9, 489, DateTimeKind.Local).AddTicks(7790)
                                });
                        });

                    b.OwnsOne("DevStore.Catalog.Domain.Specification", "Specification", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("NumberOfClasses")
                                .HasColumnName("NumberOfClasses")
                                .HasColumnType("int");

                            b1.Property<int>("TotalTime")
                                .HasColumnName("Duration")
                                .HasColumnType("int");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");

                            b1.HasData(
                                new
                                {
                                    CourseId = new Guid("aa364074-93a0-4742-85b5-2aa996857c79"),
                                    NumberOfClasses = 10,
                                    TotalTime = 100
                                },
                                new
                                {
                                    CourseId = new Guid("e0580a65-4e31-44bc-8bb5-7391892a4d56"),
                                    NumberOfClasses = 20,
                                    TotalTime = 200
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
