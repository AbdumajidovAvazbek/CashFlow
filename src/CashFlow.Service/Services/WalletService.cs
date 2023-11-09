using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Reports;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Dtos.Wallet;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Service.Services;

public class WalletService : IWalletService
{
    private readonly IRepository<Wallet> _walletRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;

    public WalletService(IMapper mapper, IRepository<Wallet> walletRepository, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _walletRepository = walletRepository;
        _userRepository = userRepository;
    }

    public async Task<WalletForResultDto> AddAsync(WalletForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var wallet = await _walletRepository.SelectAll()
            .Where(u => u.UserId == dto.UserId && u.Amount == u.Amount)
            .FirstOrDefaultAsync();

        if (wallet is not null)
            throw new CashFlowException(409, "wallet is already exist.");

        var mappedWallet = _mapper.Map<Wallet>(dto);
        mappedWallet.CreatedAt = DateTime.UtcNow;

        var result = await _walletRepository.InsertAsync(mappedWallet);

        return _mapper.Map<WalletForResultDto>(result);
    }

    public async Task<WalletForResultDto> ModifyAsync(long userId, long id, WalletForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var wallet = await _walletRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "Wallet not found");

        wallet.UpdatedAt = DateTime.UtcNow;
        var mappedWallet = _mapper.Map(dto, wallet);

        await _walletRepository.UpdateAsync(mappedWallet);

        return _mapper.Map<WalletForResultDto>(mappedWallet);
    }

    public async Task<bool> RemoveAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var wallet = await _walletRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "Wallet is not found");

        await _walletRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<WalletForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var wallet = await _walletRepository.SelectAll()
            .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<WalletForResultDto>>(wallet);
    }


    public async Task<WalletForResultDto> RetrieveByIdAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "user is not found");

        var wallet = await _walletRepository.SelectAll()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "Wallet is not found");

        return _mapper.Map<WalletForResultDto>(wallet);
    }

    public async Task<Wallet> RetrieveByIdAsync(long walletId)
    {
        var wallet = await _walletRepository.SelectAll()
                .Where(w => w.Id == walletId)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "Wallet is not found");

        return wallet;
    }
}
