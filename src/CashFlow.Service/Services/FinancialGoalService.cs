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
    private readonly IRepository<User> _userService;
    private readonly IMapper _mapper;

    public FinancialGoalService(IMapper mapper, IRepository<FinancialGoal> finansialGoal, IRepository<User> userService)
    {
        _mapper = mapper;
        _financialGoalRepository = finansialGoal;
        _userService = userService;
    }

    public async Task<FinancialGoalForResultDto> AddAsync(FinancialGoalForCreationDto dto)
    {
        var user = await _userService.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var mapperFinancial = _mapper.Map<FinancialGoal>(dto);
        mapperFinancial.CreatedAt = DateTime.UtcNow;

        var result = await _financialGoalRepository.InsertAsync(mapperFinancial);
        return _mapper.Map<FinancialGoalForResultDto>(result);

    }

    public async Task<FinancialGoalForResultDto> ModifyAsync(long userId, long id, FinancialGoalForUpdateDto dto)
    {
        var user = await _userService.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

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

    public async Task<bool> RemoveAsync(long userId, long id)
    {
        var user = await _userService.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");
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

    public async Task<FinancialGoalForResultDto> RetrieveByIdAsync(long userId,long id)
    {
        var user = await _userService.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");
        var financial = await _financialGoalRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (financial is null)
            throw new CashFlowException(404, "FinancialGoal is not found");

        return _mapper.Map<FinancialGoalForResultDto>(financial);
    }
}
