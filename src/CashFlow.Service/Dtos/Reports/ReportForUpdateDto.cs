using CashFlow.Domain.Enums;

namespace CashFlow.Service.Dtos.Reports;

public class ReportForUpdateDto
{
    public long UserId { get; set; }
    public TransactionType TransactionsType { get; set; }


}
