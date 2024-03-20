using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Application.ViewModels.Card;
using InternetBanking.Core.Application.ViewModels.Lean;
using InternetBanking.Core.Application.ViewModels.Users;
using InternetBanking.Core.Domain.Entities;
using System.ComponentModel.Design;

namespace InternetBanking.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Account
            //mapeo para guardar la cuenta de ahorro
            CreateMap<Account, SaveBankAccountViewModel>()
                .ReverseMap()
                //quitamos todas las navigation property 
                .ForMember(b => b.PaymentsMade, opt => opt.Ignore())
                .ForMember(b => b.CardPayments, opt => opt.Ignore())
                .ForMember(b => b.LoanPayments, opt => opt.Ignore())
                .ForMember(b => b.PaymentsOtherAccount, opt => opt.Ignore())
                .ForMember(b => b.Beneficiaries, opt => opt.Ignore());


            CreateMap<Account, BankAccountViewModel>();
            #endregion
            #region Card
            CreateMap<Card, SaveCardViewModel>()
                .ReverseMap()
                .ForMember(c => c.Payments, opt => opt.Ignore());
            CreateMap<Card, CardViewModel>();
            #endregion
            #region Lean
            CreateMap<Loan, LoanViewModel>();
            CreateMap<Loan, SaveLoanViewModel>()
                .ReverseMap()
                .ForMember(l => l.Payments, opt => opt.Ignore());
            #endregion
            #region Beneficiary
            CreateMap<Beneficiary, BeneficiaryViewModel>();
            CreateMap<Beneficiary, SaveBeneficiaryViewModel>()
                .ReverseMap()
                .ForMember(x => x.accountBeneficiary, opt => opt.Ignore());
            #endregion
            #region User
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}
