using CashFlow.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Domain.Entities;

public class Wallet : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Description { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}
