using CashFlow.Domain.Entities;
using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Dtos.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public ICollection<UserAssetForResultDto> userAssetsResultDto { get; set; }
    public ICollection<FinancialGoal> financialGoals { get; set; }
    public ICollection<WalletForResultDto> wallets { get; set; }
    public ICollection<Report> Reports { get; set; }
}
