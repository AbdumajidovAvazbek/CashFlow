﻿using CashFlow.Domain.Commons;
using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Entities;

public class Report : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public TransactionType TransactionType { get; set; }
}
    