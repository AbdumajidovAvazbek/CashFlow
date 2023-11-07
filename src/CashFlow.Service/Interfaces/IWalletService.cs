using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Interfaces;

public interface IWalletService
{
    public Task<bool> RemoveAsync(long id);
    public Task<WalletForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<WalletForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<WalletForResultDto> ModifyAsync(long id, WalletForUpdateDto dto);
    public Task<WalletForResultDto> CreateAsync(WalletForUpdateDto dto);
}
