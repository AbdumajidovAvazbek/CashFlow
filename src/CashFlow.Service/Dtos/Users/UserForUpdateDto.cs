using CashFlow.Domain.Enums;

namespace CashFlow.Service.Dtos.Users;

public class UserForUpdateDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public GenderType Gender { get; set; }

}
