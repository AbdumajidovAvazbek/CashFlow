﻿using CashFlow.Domain.Enums;
using CashFlow.Domain.Commons;

namespace CashFlow.Domain.Entities;

public class User : Auditable
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public GenderType Type { get; set; }
}
