using CashFlow.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Service.Dtos.Transactions;

public class TransactionForUpdateDto
{
    [Required]
    public long WalletId { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

}
