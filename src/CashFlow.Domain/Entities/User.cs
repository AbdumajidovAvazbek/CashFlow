using CashFlow.Domain.Enums;
using CashFlow.Domain.Commons;

namespace CashFlow.Domain.Entities;

public class User : Auditable
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public GenderType Type { get; set; }
    public ICollection<UserAsset> userAssets { get; set; }
    public ICollection<FinancialGoal> financialGoals { get; set; }
    public ICollection<Wallet> wallets { get; set; }
    public ICollection<Report> Reports { get; set; }
}
