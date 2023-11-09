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
        private readonly IRepository<Report> _reportRepository;

        public TransactionService(IMapper mapper, IRepository<Transaction> transactionRepository, IRepository<Wallet> walletRepository, IRepository<Report> reportRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _reportRepository = reportRepository;
        }

        public async Task<TransactionForResultDto> AddAsync(TransactionForCreationDto dto)
        {
            // Check if the wallet exists
            var wallet = await _walletRepository.SelectAll()
                .Where(u => u.Id == dto.WalletId)
                .FirstOrDefaultAsync();

            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");
            // Map the DTO to an entity

            // Calculate wallet amount based on transaction type
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
            var mappedTransaction = _mapper.Map<Transaction>(dto);
            mappedTransaction.CreatedAt = DateTime.UtcNow;

            var result = await _transactionRepository.InsertAsync(mappedTransaction);

            Report report = new Report();
            report.UserId = wallet.UserId;
            report.CreatedAt = DateTime.UtcNow;
            report.TransactionType = mappedTransaction.Type;
            await _reportRepository.InsertAsync(report);

            result.Type = dto.Type;
            wallet.UpdatedAt = DateTime.UtcNow;

            await _walletRepository.UpdateAsync(wallet);

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

            // Map the DTO to the transaction entity and update it
            var model = _mapper.Map(dto, transaction);
            model.UpdatedAt = DateTime.UtcNow;

            // Update wallet amount based on transaction type
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
            model.Wallet = wallet;

            await _transactionRepository.UpdateAsync(model);

            return _mapper.Map<TransactionForResultDto>(model);
        }

        public async Task<bool> RemoveAsync(long walletId, long id)
        {
            var wallet = await _walletRepository.SelectAll()
               .Where(u => u.Id == walletId)
               .FirstOrDefaultAsync();
            if (wallet is null)
                throw new CashFlowException(404, "Wallet is not found");

            var transaction = await _transactionRepository.SelectAll()
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync();
            if (transaction is null)
                throw new CashFlowException(404, "Transaction is not found");

            await _transactionRepository.DeleteAsync(id);

            return true;
        }

        public async Task<IEnumerable<TransactionForResultDto>> RetrieveAllAsync(PaginationParams @params)
        {
            var transactions = await _transactionRepository.SelectAll()
                .ToPagedList(@params)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TransactionForResultDto>>(transactions);
        }

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
