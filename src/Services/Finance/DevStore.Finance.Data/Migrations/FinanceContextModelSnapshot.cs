﻿// <auto-generated />
using System;
using DevStore.Finance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevStore.Finance.Data.Migrations
{
    [DbContext(typeof(FinanceContext))]
    partial class FinanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevStore.Finance.Business.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CvvCard")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("ExpirationDateCard")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("NameCard")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("NumberCard")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DevStore.Finance.Business.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusTransaction")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DevStore.Finance.Business.Transaction", b =>
                {
                    b.HasOne("DevStore.Finance.Business.Payment", "Payment")
                        .WithOne("Transaction")
                        .HasForeignKey("DevStore.Finance.Business.Transaction", "PaymentId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
