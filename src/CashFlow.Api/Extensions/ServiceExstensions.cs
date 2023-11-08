using CashFlow.Service.Services;
using CashFlow.Data.IRepositories;
using CashFlow.Service.Interfaces;
using CashFlow.Data.Repositories;
using CashFlow.Service.Helpers;

namespace CashFlow.Api.Extensions;

public static class ServiceExstensions
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFinancialGoalService, FinancialGoalService > ();
        services.AddScoped<IWalletService, WalletService>();    
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IUserAssetService, UserAssetService>();
    }
}
