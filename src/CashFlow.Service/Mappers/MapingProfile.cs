using AutoMapper;
using CashFlow.Domain.Entities;
using CashFlow.Service.Dtos.FinancialGoals;
using CashFlow.Service.Dtos.Reports;
using CashFlow.Service.Dtos.Transactions;
using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Dtos.Wallet;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();

        // FinancialGoal
        CreateMap<FinancialGoal, FinancialGoalForResultDto>().ReverseMap();
        CreateMap<FinancialGoal, FinancialGoalForUpdateDto>().ReverseMap();
        CreateMap<FinancialGoal, FinancialGoalForCreationDto>().ReverseMap();

        // Transaction
        CreateMap<Transaction, TransactionForUpdateDto>().ReverseMap();
        CreateMap<Transaction, TransactionForResultDto>().ReverseMap();
        CreateMap<Transaction, TransactionForCreationDto>().ReverseMap();

        // Wallet
        CreateMap<Wallet, WalletForResultDto>().ReverseMap();
        CreateMap<Wallet, WalletForUpdateDto>().ReverseMap();
        CreateMap<WalletForCreationDto, Wallet>(); 

        // Report
        CreateMap<Report, ReportForResultDto>().ReverseMap();
        CreateMap<Report, ReportForUpdateDto>().ReverseMap();

        // UserAsset
        CreateMap<UserAsset, UserAssetForResultDto>().ReverseMap();
    }
}
