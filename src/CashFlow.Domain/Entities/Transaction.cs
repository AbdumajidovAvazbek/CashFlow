using CashFlow.Domain.Enums;
using CashFlow.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Domain.Entities;

public class Transaction : Auditable
{
    public long WalletId { get; set; }
    public Wallet Wallet { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
}
