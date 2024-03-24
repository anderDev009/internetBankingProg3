﻿// <auto-generated />
using System;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(InternetBankingContext))]
    [Migration("20240319131005_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Account", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<decimal>("InitialAmmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsMainAccount")
                        .HasColumnType("bit");

                    b.HasKey("Code");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber");

                    b.ToTable("Beneficiary");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AmountAvailable")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<decimal>("Limit")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CardPay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdAccountPaid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdCard")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAccountPaid");

                    b.HasIndex("IdCard");

                    b.ToTable("CardPay");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BalanceLoan")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<decimal>("LoanUser")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.LoanPay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountPaidCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdAccountPaid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdLoan")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountPaidCode");

                    b.HasIndex("IdLoan");

                    b.ToTable("LoanPay");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.PayExpress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdAccountPaid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("IdAccountPaid");

                    b.ToTable("PayExpress");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiary", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.Account", "accountBeneficiary")
                        .WithMany("Beneficiaries")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("accountBeneficiary");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CardPay", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.Account", "AccountPaid")
                        .WithMany("CardPayments")
                        .HasForeignKey("IdAccountPaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.Card", "Card")
                        .WithMany("Payments")
                        .HasForeignKey("IdCard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountPaid");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.LoanPay", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.Account", "AccountPaid")
                        .WithMany("LoanPayments")
                        .HasForeignKey("AccountPaidCode");

                    b.HasOne("InternetBanking.Core.Domain.Entities.Loan", "Loan")
                        .WithMany("Payments")
                        .HasForeignKey("IdLoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountPaid");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.PayExpress", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.Account", "AccountPaymentMade")
                        .WithMany("PaymentsMade")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.Account", "AccountPaid")
                        .WithMany("PaymentsOtherAccount")
                        .HasForeignKey("IdAccountPaid")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AccountPaid");

                    b.Navigation("AccountPaymentMade");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Account", b =>
                {
                    b.Navigation("Beneficiaries");

                    b.Navigation("CardPayments");

                    b.Navigation("LoanPayments");

                    b.Navigation("PaymentsMade");

                    b.Navigation("PaymentsOtherAccount");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Card", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Loan", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
