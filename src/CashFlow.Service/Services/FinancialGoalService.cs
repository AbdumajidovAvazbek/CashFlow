using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.FinancialGoals;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Dtos.Wallet;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Service.Services;

public class FinancialGoalService : IFinancialGoalService
{
    private readonly IRepository<FinancialGoal> _financialGoalRepository;
    private readonly IMapper _mapper;

    public FinancialGoalService(IMapper mapper, IRepository<FinancialGoal> finansialGoal)
    {
        _mapper = mapper;
        _financialGoalRepository = finansialGoal;
    }

    public async Task<FinancialGoalForResultDto> AddAsync(FinancialGoalForCreationDto dto)
    {
        var finansial = await _financialGoalRepository.SelectAll()
            .Where(f => f.UserId == dto.UserId && f.Name == dto.Name)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (finansial is not null)
            throw new CashFlowException(409, "FinancialGoal  is already exist.");

        var mapperFinancial = _mapper.Map<FinancialGoal>(dto);
        mapperFinancial.CreatedAt = DateTime.UtcNow;

        var result = await _financialGoalRepository.InsertAsync(mapperFinancial);
        return _mapper.Map<FinancialGoalForResultDto>(result);

    }

    public async Task<FinancialGoalForResultDto> ModifyAsync(long id, FinancialGoalForUpdateDto dto)
    {
        var financial = await _financialGoalRepository.SelectAll()
               .Where(u => u.Id == id)
               .FirstOrDefaultAsync();
        if (financial is null)
            throw new CashFlowException(404, "financialGoal not found");

        financial.UpdatedAt = DateTime.UtcNow;
        var mappedFinancial = _mapper.Map(dto, financial);

        await _financialGoalRepository.UpdateAsync(mappedFinancial);

        return _mapper.Map<FinancialGoalForResultDto>(mappedFinancial);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var financial = await _financialGoalRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (financial is null)
            throw new CashFlowException(404, "FinancialGoal is not found");

        return await _financialGoalRepository.DeleteAsync(id);

    }

    public async Task<IEnumerable<FinancialGoalForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var financials = await _financialGoalRepository.SelectAll()
            .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<FinancialGoalForResultDto>>(financials);
    }

    public async Task<FinancialGoalForResultDto> RetrieveByIdAsync(long id)
    {
        var financial = await _financialGoalRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (financial is null)
            throw new CashFlowException(404, "FinancialGoal is not found");

        return _mapper.Map<FinancialGoalForResultDto>(financial);
    }
}
