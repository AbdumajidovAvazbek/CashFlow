﻿namespace CashFlow.Service.Dtos.UserAssets;

public class UserAssetForResult
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
}
