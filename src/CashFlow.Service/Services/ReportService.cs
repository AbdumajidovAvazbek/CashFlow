using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.FinancialGoals;
using CashFlow.Service.Dtos.Reports;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Helpers;
using CashFlow.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Service.Services;

public class ReportService : IReportService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Report> _reportRepository;
    private readonly IRepository<User> _userRepository;

    public ReportService(IMapper mapper, IRepository<Report> reportRepository, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _reportRepository = reportRepository;
        _userRepository = userRepository;
    }
    public async Task<ReportForResultDto> ModifyAsync(long userId, long id, ReportForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var report = await _reportRepository.SelectAll()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
        if (report is null)
            throw new CashFlowException(404, "Report is not found ");

        var result = _mapper.Map(dto, report);
        result.UpdatedAt = DateTime.UtcNow;

        await _reportRepository.UpdateAsync(result);

        return _mapper.Map<ReportForResultDto>(result);
    }
    public async Task<bool> RemoveAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");
        var report = await _reportRepository.SelectAll()
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
        if (report is null)
            throw new CashFlowException(404, "Report is not found");

        await _reportRepository.DeleteAsync(id);

        return true;
    }
    public async Task<IEnumerable<ReportForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var report = await _reportRepository.SelectAll()
            .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<ReportForResultDto>>(report);
    }

    public async Task<ReportForResultDto> RetrieveByIdAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var report = await _reportRepository.SelectAll()
               .Where(u => u.Id == id)
               .FirstOrDefaultAsync();
        if (report is null)
            throw new CashFlowException(404, "report is not found");

        return _mapper.Map<ReportForResultDto>(report);
    }
}
