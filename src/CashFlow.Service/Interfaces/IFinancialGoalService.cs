using CashFlow.Service.Dtos.FinancialGoals;

namespace CashFlow.Service.Interfaces;

public interface IFinancialGoalService
{
    public Task<bool> RemoveAsync(long id);
    public Task<FinancialGoalForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<FinancialGoalForResultDto>> RetrieveAllAsync();
    public Task<FinancialGoalForResultDto> ModifyAsync(FinancialGoalForUpdateDto dto);
    public Task<FinancialGoalForResultDto> CreateAsync(FinancialGoalForCreationDto dto);
}
