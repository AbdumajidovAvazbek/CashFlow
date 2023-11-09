using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Reports;

namespace CashFlow.Service.Interfaces;

public interface IReportService
{
    public Task<bool> RemoveAsync(long userId, long id);
    public Task<ReportForResultDto> RetrieveByIdAsync(long userId, long id);
    public Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<ReportForResultDto> ModifyAsync(long userId, long id, ReportForUpdateDto dto);
}
