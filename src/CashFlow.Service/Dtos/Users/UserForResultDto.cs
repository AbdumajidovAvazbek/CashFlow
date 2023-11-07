using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Dtos.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public IEnumerable<UserAssetForResultDto> UserAssetForResults {  get; set; }
    
    public IEnumerable<WalletForResultDto> WalletForResults { get; set; }
}
