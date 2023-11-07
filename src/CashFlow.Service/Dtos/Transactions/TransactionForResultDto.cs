using CashFlow.Domain.Enums;
using CashFlow.Service.Dtos.Wallet;

namespace CashFlow.Service.Dtos.Transactions;

public class TransactionForResultDto
{
    public long Id { get; set; }

    public WalletForResultDto wallet { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; }
}
