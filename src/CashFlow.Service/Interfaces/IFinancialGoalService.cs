using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.FinancialGoals;

namespace CashFlow.Service.Interfaces;

public interface IFinancialGoalService
{
    public Task<bool> RemoveAsync(long userId, long id);
    public Task<FinancialGoalForResultDto> RetrieveByIdAsync(long userId, long id);
    public Task<IEnumerable<FinancialGoalForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<FinancialGoalForResultDto> ModifyAsync(long userId, long id, FinancialGoalForUpdateDto dto);
    public Task<FinancialGoalForResultDto> AddAsync(FinancialGoalForCreationDto dto);
}
