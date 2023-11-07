using CashFlow.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Service.Dtos.Transactions;

public class TransactionForCreationDto
{
    [Required]
    public long WalletId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Required]
    public decimal Amount { get; set; }
    public string? Description { get; set; }

    [Required]
    public TransactionType Type { get; set; }
}
