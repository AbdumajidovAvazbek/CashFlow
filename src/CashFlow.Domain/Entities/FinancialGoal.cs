using CashFlow.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Domain.Entities;

public class FinancialGoal : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
}
