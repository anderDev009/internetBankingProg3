

using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class InternetBankingContext : DbContext
    {
        public InternetBankingContext(DbContextOptions<InternetBankingContext> options) : base(options)
        {}

        public DbSet<Account> Account { get; set; }
        public DbSet<Beneficiary> Beneficiary { get; set; } 
        public DbSet<Card> Card { get; set; }
        public DbSet<CardPay> CardPay { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanPay> LoanPay { get; set; }
        public DbSet<PayExpress> PayExpress { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region renaming tables 
            #endregion
            base.OnModelCreating(modelBuilder);
            #region keys
            modelBuilder.Entity<Account>().HasKey(x => x.Code);
            modelBuilder.Entity<Beneficiary>().HasKey(x => x.Id);
            modelBuilder.Entity<Card>().HasKey(x => x.Id);
            modelBuilder.Entity<CardPay>().HasKey(x => x.Id);
            modelBuilder.Entity<Loan>().HasKey(x => x.Id);
            modelBuilder.Entity<LoanPay>().HasKey(x => x.Id);
            modelBuilder.Entity<PayExpress>().HasKey(x => x.Id);
            #endregion
            #region Foreign Keys
            #region Account
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Beneficiaries)
                .WithOne(b => b.accountBeneficiary)
                .HasForeignKey(b => b.AccountNumber);
            #endregion
//s
            #endregion
        }
    }
}
