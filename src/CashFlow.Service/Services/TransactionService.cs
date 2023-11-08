using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Transactions;
using CashFlow.Service.Dtos.Wallet;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Service.Services;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Transaction> _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(IMapper mapper, IRepository<Transaction> transactionRepository)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionForResultDto> AddAsync(TransactionForCreationDto dto)
    {
        var users = await _transactionRepository.SelectAll()
            .Where(t => t.WalletId == dto.WalletId)
            .FirstOrDefaultAsync();

        if (users is not null)
            throw new CashFlowException(409, "Transaction is already exist.");

        var mappedTransaction = _mapper.Map<Transaction>(dto);
        mappedTransaction.CreatedAt = DateTime.UtcNow;

        var result = await _transactionRepository.InsertAsync(mappedTransaction);

        return _mapper.Map<TransactionForResultDto>(result);
    }

    public async Task<TransactionForResultDto> ModifyAsync(long id, TransactionForUpdateDto dto)
    {
        var transaction = await _transactionRepository.SelectAll()
                .Where(u => u.Id == id)
                .Include(w => w.Wallet)
                .FirstOrDefaultAsync();
        if (transaction is null)
            throw new CashFlowException(404, "transaction not found");

        var model = _mapper.Map(dto, transaction);
        model.UpdatedAt = DateTime.UtcNow;

        await _transactionRepository.UpdateAsync(model);

        return _mapper.Map<TransactionForResultDto>(model);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var transaction = await _transactionRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        if (transaction is null)
            throw new CashFlowException(404, "transaction is not found");

        await _transactionRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var transactions = _transactionRepository.SelectAll()
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<TransactionForResultDto>>(transactions);
    }


    public async Task<TransactionForResultDto> RetrieveByIdAsync(long id)
    {
        var wallet = await _transactionRepository.SelectAll()
                .Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        if (wallet is null)
            throw new CashFlowException(404, "transaction is not found");

        return _mapper.Map<TransactionForResultDto>(wallet);
    }
}
