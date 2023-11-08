using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Service.Dtos.Wallet;

public class WalletForUpdateDto
{
    public long UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
}
