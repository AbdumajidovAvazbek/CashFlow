using CashFlow.Service.Dtos.Transactions;

namespace CashFlow.Service.Interfaces;

public interface ITransactionService
{
    public Task<bool> RemoveAsync(long id);
    public Task<TransactionForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync();
    public Task<TransactionForResultDto> ModifyAsync(long id, TransactionForUpdateDto dto);
    public Task<TransactionForResultDto> CreateAsync(TransactionForCreationDto dto);
}
