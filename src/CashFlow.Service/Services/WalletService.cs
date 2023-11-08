using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
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

    public WalletService(IMapper mapper, IRepository<Wallet> userRepository)
    {
        _mapper = mapper;
        _walletRepository = userRepository;
    }

    public async Task<WalletForResultDto> AddAsync(WalletForCreationDto dto)
    {
        var users = await _walletRepository.SelectAll()
            .Where(u => u.UserId == dto.UserId && u.Amount == u.Amount)
            .FirstOrDefaultAsync();

        if (users is not null)
            throw new CashFlowException(409, "User is already exist.");

        var mappedWallet = _mapper.Map<Wallet>(dto);
        mappedWallet.CreatedAt = DateTime.UtcNow;

        var result = await _walletRepository.InsertAsync(mappedWallet);

        return _mapper.Map<WalletForResultDto>(result);
    }

    public async Task<WalletForResultDto> ModifyAsync(long id, WalletForUpdateDto dto)
    {
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

    public async Task<bool> RemoveAsync(long id)
    {
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
        var wallet = _walletRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<WalletForResultDto>>(wallet);
    }


    public async Task<WalletForResultDto> RetrieveByIdAsync(long id)
    {
        var wallet = await _walletRepository.SelectAll()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "Wallet is not found");

        return _mapper.Map<WalletForResultDto>(wallet);
    }
}
