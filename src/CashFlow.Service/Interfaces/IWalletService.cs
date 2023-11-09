using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Interfaces;

public interface IWalletService
{
    public Task<bool> RemoveAsync(long userId, long id);
    public Task<WalletForResultDto> RetrieveByIdAsync(long userId, long id);
    public Task<IEnumerable<WalletForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<WalletForResultDto> ModifyAsync(long userId, long id, WalletForUpdateDto dto);
    public Task<WalletForResultDto> AddAsync(WalletForCreationDto dto);
    public Task <Wallet> RetrieveByIdAsync(long walletId);
}
