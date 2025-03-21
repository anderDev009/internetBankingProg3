﻿
using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.ViewModels.BankAccount;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiaryService : BaseService<BeneficiaryViewModel, SaveBeneficiaryViewModel, Beneficiary>,
                                      IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IBankAccountService _bankAccountService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService; 
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository,
                                   IMapper mapper,
                                   IBankAccountService bankAccountService,
                                   IAccountService accountService)
               : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _mapper = mapper;
            _bankAccountService = bankAccountService;
            _accountService = accountService;
        }
       
        public async Task<List<BeneficiaryViewModel>> GetBeneficiaryByIdUser(string IdUser)
        {
            List<Beneficiary> beneficiaries = await _beneficiaryRepository.GetBeneficiaryByIdUser(IdUser);
            return  _mapper.Map<List<BeneficiaryViewModel>>(beneficiaries);
        }

        public override async Task<SaveBeneficiaryViewModel> SaveAsync(SaveBeneficiaryViewModel vm)
        {
            SaveBankAccountViewModel account = await _bankAccountService.GetByIdAsync(vm.AccountNumber);
            if(account == null)
            {
                throw new Exception("Esta cuenta no existe");
            }
  
            return await base.SaveAsync(vm);
        }
    }
}
