﻿// <auto-generated />
using System;
using DevStore.Catalog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevStore.Catalog.Data.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20210221152537_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Id = new Guid("14f864db-935e-4fb1-9312-8d10de05f1c0"),
                            Code = 1,
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = new Guid("d48f9583-08c2-4a74-b39c-4327893268a6"),
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
                            Id = new Guid("6d36cf8f-d367-4cef-83a1-277d9165cc6f"),
                            CategoryId = new Guid("14f864db-935e-4fb1-9312-8d10de05f1c0"),
                            CreatedDate = new DateTime(2021, 2, 21, 15, 25, 37, 89, DateTimeKind.Local).AddTicks(1636),
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
                            Id = new Guid("731b46c1-b3d2-4ac8-9151-32fc58fa3863"),
                            CategoryId = new Guid("d48f9583-08c2-4a74-b39c-4327893268a6"),
                            CreatedDate = new DateTime(2021, 2, 21, 15, 25, 37, 92, DateTimeKind.Local).AddTicks(4661),
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
                                    CourseId = new Guid("6d36cf8f-d367-4cef-83a1-277d9165cc6f"),
                                    EndDate = new DateTime(2021, 3, 21, 15, 25, 37, 86, DateTimeKind.Local).AddTicks(3237),
                                    StartDate = new DateTime(2021, 2, 21, 15, 25, 37, 80, DateTimeKind.Local).AddTicks(3932)
                                },
                                new
                                {
                                    CourseId = new Guid("731b46c1-b3d2-4ac8-9151-32fc58fa3863"),
                                    EndDate = new DateTime(2021, 4, 21, 15, 25, 37, 88, DateTimeKind.Local).AddTicks(328),
                                    StartDate = new DateTime(2021, 2, 21, 15, 25, 37, 88, DateTimeKind.Local).AddTicks(270)
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
                                    CourseId = new Guid("6d36cf8f-d367-4cef-83a1-277d9165cc6f"),
                                    NumberOfClasses = 10,
                                    TotalTime = 100
                                },
                                new
                                {
                                    CourseId = new Guid("731b46c1-b3d2-4ac8-9151-32fc58fa3863"),
                                    NumberOfClasses = 20,
                                    TotalTime = 200
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}