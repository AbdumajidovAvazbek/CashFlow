using CashFlow.Domain.Enums;

namespace CashFlow.Service.Dtos.Users;

public class UserForCreationDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public GenderType Type { get; set; }
}
