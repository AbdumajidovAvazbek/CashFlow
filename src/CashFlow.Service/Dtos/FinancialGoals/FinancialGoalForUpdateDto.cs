using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Service.Dtos.FinancialGoals;

public class FinancialGoalForUpdateDto
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
}
