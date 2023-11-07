using CashFlow.Service.Services;
using CashFlow.Data.IRepositories;
using CashFlow.Service.Interfaces;
using CashFlow.Data.Repositories;

namespace CashFlow.Api.Extensions;

public static class ServiceExstensions
{
    public static void AddCustomService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    }
}
