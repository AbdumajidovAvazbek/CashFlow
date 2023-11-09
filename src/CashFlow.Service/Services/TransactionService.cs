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

namespace CashFlow.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Wallet> _walletRepository;

        public TransactionService(IMapper mapper, IRepository<Transaction> transactionRepository, IRepository<Wallet> walletRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        // Method 1: Add a new transaction
        public async Task<TransactionForResultDto> AddAsync(TransactionForCreationDto dto)
        {
            // Check if the wallet exists
            var wallet = await _walletRepository.SelectAll()
                .Where(u => u.Id == dto.WalletId)
                .FirstOrDefaultAsync();
            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");

            // Map the DTO to an entity
            var mappedTransaction = _mapper.Map<Transaction>(dto);
            mappedTransaction.CreatedAt = DateTime.UtcNow;

            // Calculate wallet amount based on transaction type
            var result = await _transactionRepository.InsertAsync(mappedTransaction);
            if ((int)dto.Type == 1)
            {
                wallet.Amount += dto.Amount;
            }
            else if ((int)dto.Type == 2)
            {
                if (wallet.Amount - dto.Amount > 0)
                {
                    wallet.Amount -= dto.Amount;
                }
                else
                {
                    throw new CashFlowException(400, "Amount in Wallet is not enough");
                }
            }
            result.Wallet = wallet;

            return _mapper.Map<TransactionForResultDto>(result);
        }

        // Method 2: Modify an existing transaction
        public async Task<TransactionForResultDto> ModifyAsync(long walletId, long id, TransactionForUpdateDto dto)
        {
            // Check if the wallet exists
            var wallet = await _walletRepository.SelectAll()
                .Where(u => u.Id == walletId)
                .FirstOrDefaultAsync();
            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");

            // Check if the transaction exists
            var transaction = await _transactionRepository.SelectAll()
                    .Where(u => u.Id == id)
                    .Include(w => w.Wallet)
                    .FirstOrDefaultAsync();
            if (transaction is null)
                throw new CashFlowException(404, "Transaction not found");

            // Map the DTO to the transaction entity and update it
            var model = _mapper.Map(dto, transaction);
            model.UpdatedAt = DateTime.UtcNow;

            // Update wallet amount based on transaction type
            if ((int)dto.Type == 10)
            {
                wallet.Amount += dto.Amount;
            }
            else if ((int)dto.Type == 20)
            {
                if (wallet.Amount - dto.Amount > 0)
                {
                    wallet.Amount -= dto.Amount;
                }
                else
                {
                    throw new CashFlowException(400, "Amount in Wallet is not enough");
                }
            }
            model.Wallet = wallet;

            await _transactionRepository.UpdateAsync(model);

            return _mapper.Map<TransactionForResultDto>(model);
        }

        // Method 3: Remove a transaction
        public async Task<bool> RemoveAsync(long walletId, long id)
        {
            // Check if the wallet exists
            var wallet = await _walletRepository.SelectAll()
               .Where(u => u.Id == walletId)
               .FirstOrDefaultAsync();
            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");

            // Check if the transaction exists
            var transaction = await _transactionRepository.SelectAll()
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();
            if (transaction is null)
                throw new CashFlowException(404, "Transaction is not found");

            // Delete the transaction
            await _transactionRepository.DeleteAsync(id);

            return true;
        }

        // Method 4: Retrieve a list of transactions
        public async Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var transactions = await _transactionRepository.SelectAll()
                .ToPagedList(@params)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TransactionForResultDto>>(transactions);
        }

        // Method 5: Retrieve a specific transaction by ID
        public async Task<TransactionForResultDto> RetrieveByIdAsync(long walletId, long id)
        {
            // Check if the wallet exists
            var wallet = await _walletRepository.SelectAll()
               .Where(u => u.Id == walletId)
               .FirstOrDefaultAsync();
            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");

            // Check if the transaction exists
            var transaction = await _transactionRepository.SelectAll()
                    .Where(w => w.Id == id)
                    .FirstOrDefaultAsync();
            if (transaction is null)
                throw new CashFlowException(404, "Transaction is not found");

            return _mapper.Map<TransactionForResultDto>(transaction);
        }
    }
}
