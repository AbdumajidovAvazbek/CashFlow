using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Reports;

namespace CashFlow.Service.Interfaces;

public interface IReportService
{
    public Task<bool> RemoveAsync(long id);
    public Task<ReportForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<ReportForResultDto> ModifyAsync(long id, ReportForUpdateDto dto);
}
