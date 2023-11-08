using CashFlow.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Service.Dtos.Wallet;

public class WalletForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
