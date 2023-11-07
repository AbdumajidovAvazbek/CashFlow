﻿using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Users;

namespace CashFlow.Service.Interfaces;

public interface IUserService
{
    public Task<bool> RemoveAsync(long id);
    public Task<UserForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    public Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
}