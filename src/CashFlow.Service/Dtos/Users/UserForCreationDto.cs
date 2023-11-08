using CashFlow.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Service.Dtos.Users;

public class UserForCreationDto
{
    [Required]
    public string Name { get; set; }
    public string? Surname { get; set; }

    [EmailAddress(ErrorMessage = "email required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required]
    public GenderType Type { get; set; }
}
