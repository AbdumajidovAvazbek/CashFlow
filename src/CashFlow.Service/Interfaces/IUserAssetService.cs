using Microsoft.AspNetCore.Http;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.UserAssets;

namespace CashFlow.Service.Interfaces;

public interface IUserAssetService
{
    Task<bool> RemoveAsync(long userId, long id);
    Task<UserAssetForResultDto> RetrieveByIdAsync(long userId, long id);
    Task<UserAssetForResultDto> CreateAsync(IFormFile formFile);
    Task<IEnumerable<UserAssetForResultDto>> RetrieveAllAsync(long userId, PaginationParams @params);
}
