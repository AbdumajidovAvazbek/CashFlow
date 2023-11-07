using CashFlow.Domain.Enums;

namespace CashFlow.Service.Dtos.Reports;

public class ReportForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public TransactionType TransactionsType { get; set; }
}
