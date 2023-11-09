using CashFlow.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CashFlow.Domain.Entities;

public class Wallet : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [JsonIgnore]
    public ICollection<Transaction> Transactions { get; set; }
}
