using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Transactions;

namespace CashFlow.Service.Interfaces;

public interface ITransactionService
{
    public Task<bool> RemoveAsync(long walletId, long id);
    public Task<TransactionForResultDto> RetrieveByIdAsync(long walletId, long id);
    public Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<TransactionForResultDto> ModifyAsync(long walletId, long id, TransactionForUpdateDto dto);
    public Task<TransactionForResultDto> AddAsync(TransactionForCreationDto dto);
}
