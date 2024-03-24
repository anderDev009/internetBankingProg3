

using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class InternetBankingContext : DbContext
    {
        public InternetBankingContext(DbContextOptions<InternetBankingContext> options) : base(options)
        { }

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
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Beneficiaries)
                .WithOne(b => b.accountBeneficiary)
                .HasForeignKey(b => b.AccountNumber);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.CardPayments)
                .WithOne(c => c.AccountPaid)
                .HasForeignKey(c => c.IdAccountPaid);

            //cuenta a la que se realizo el pago 
            modelBuilder.Entity<Account>()
                .HasMany(a => a.PaymentsMade)
                .WithOne(p => p.AccountPaymentMade)
                .HasForeignKey(p => p.AccountNumber).OnDelete(DeleteBehavior.NoAction);
            //cuenta  que pago
            modelBuilder.Entity<Account>()
               .HasMany(a => a.PaymentsOtherAccount)
               .WithOne(p => p.AccountPaid)
               .HasForeignKey(p => p.IdAccountPaid).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region Card
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.IdCard);
            #endregion

            #region Loan
            modelBuilder.Entity<Loan>()
                .HasMany(l => l.Payments)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.IdLoan);
            #endregion
            #endregion
            #region Props
            #region Account
            modelBuilder.Entity<Account>().Property(a => a.Balance).HasPrecision(18, 2).IsRequired(); 
            modelBuilder.Entity<Account>().Property(a => a.IdUser).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.Code).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.InitialAmmount).HasPrecision(18, 2).IsRequired();
            #endregion Beneficiary
            modelBuilder.Entity<Beneficiary>().Property(b => b.AccountNumber).IsRequired();
            modelBuilder.Entity<Beneficiary>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Beneficiary>().Property(b => b.LastName).IsRequired();
            modelBuilder.Entity<Beneficiary>().Property(b => b.IdUser).IsRequired();
            #region Card
            modelBuilder.Entity<Card>().Property(c => c.AmountAvailable).HasPrecision(18, 2).IsRequired();
            modelBuilder.Entity<Card>().Property(c => c.Limit).HasPrecision(18, 2).IsRequired();
            modelBuilder.Entity<Card>().Property(c => c.IdUser).IsRequired();
            #endregion
            #region CardPay
            modelBuilder.Entity<CardPay>().Property(c => c.IdCard).IsRequired();
            modelBuilder.Entity<CardPay>().Property(c => c.IdAccountPaid).IsRequired();
            modelBuilder.Entity<CardPay>().Property(c => c.Amount).HasPrecision(18,2).IsRequired();
            #endregion
            #region Loan
            modelBuilder.Entity<Loan>().Property(l => l.BalanceLoan).HasPrecision(18, 2).IsRequired();
            modelBuilder.Entity<Loan>().Property(l => l.IdUser).IsRequired();
            modelBuilder.Entity<Loan>().Property(l => l.LoanUser).HasPrecision(18, 2).IsRequired();
            #endregion
            #region LoanPay
            modelBuilder.Entity<LoanPay>().Property(l => l.IdLoan).IsRequired();
            modelBuilder.Entity<LoanPay>().Property(l => l.Amount).HasPrecision(18, 2).IsRequired();
            modelBuilder.Entity<LoanPay>().Property(l => l.IdAccountPaid).IsRequired();
            #endregion
            #region PayExpress
            modelBuilder.Entity<PayExpress>().Property(p => p.AccountNumber).IsRequired();
            modelBuilder.Entity<PayExpress>().Property(p => p.IdAccountPaid).IsRequired();
            modelBuilder.Entity<PayExpress>().Property(p => p.Amount).HasPrecision(18,2).IsRequired();
            #endregion 
            #endregion
        }
    }
}
