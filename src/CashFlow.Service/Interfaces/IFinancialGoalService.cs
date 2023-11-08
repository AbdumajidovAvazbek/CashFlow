using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.FinancialGoals;

namespace CashFlow.Service.Interfaces;

public interface IFinancialGoalService
{
    public Task<bool> RemoveAsync(long id);
    public Task<FinancialGoalForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<FinancialGoalForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<FinancialGoalForResultDto> ModifyAsync(long id, FinancialGoalForUpdateDto dto);
    public Task<FinancialGoalForResultDto> AddAsync(FinancialGoalForCreationDto dto);
}
