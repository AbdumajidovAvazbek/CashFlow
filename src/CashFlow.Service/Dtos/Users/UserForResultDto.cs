using CashFlow.Domain.Entities;
using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Dtos.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string Username { get; set; }
    public ICollection<UserAssetForResultDto> Assets { get; set; }

}
