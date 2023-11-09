using AutoMapper;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CashFlow.Data.IRepositories;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Helpers;
using System.Security.Claims;
using CashFlow.Service.Dtos.Reports;

namespace Shamsheer.Service.Services.UserAssets;

public class UserAssetService : IUserAssetService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserAsset> _userAssetRepository;
    private readonly IRepository<Asset> _assetRepository;

    public UserAssetService(IMapper mapper, IRepository<User> userRepository, IRepository<UserAsset> userAssetRepository, IRepository<Asset> assetRepository )
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _userAssetRepository = userAssetRepository;
        _assetRepository = assetRepository;
    }
    public async Task<UserAssetForResultDto> AddAsync(IFormFile formFile)
    {
        //Identify UserId TODO:LOGIC
        long userId = (long)HttpContextHelper.UserId;

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(WebHostEnvironmentHelper.WebRootPath, "Media", "ProfilePictures", "Users", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserAsset()
        {
            UserId = userId,
            Name = fileName,
            Path = Path.Combine("Media", "ProfilePictures", "Users", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userAssetRepository.InsertAsync(mappedAsset);

        return _mapper.Map<UserAssetForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new CashFlowException(404, "User is not found.");

        var userAsset = await _userAssetRepository.SelectAll()
            .Where(ur => ur.Id == id)
            .FirstOrDefaultAsync();
        if (userAsset is null)
            throw new CashFlowException(404, "User Asset is not found.");


        await _userAssetRepository.DeleteAsync(userAsset.Id);

        return true;
    }

    public async Task<IEnumerable<UserAssetForResultDto>> RetrieveAllAsync(long userId, PaginationParams @params)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new CashFlowException(404, "User is not found.");
        var userAsset = await _assetRepository.SelectAll()
            .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<UserAssetForResultDto>>(userAsset);
    }

    public async Task<UserAssetForResultDto> RetrieveByIdAsync(long userId, long id)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            throw new CashFlowException(404, "User is not found.");

        var userAsset = await _userAssetRepository.SelectAll()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (userAsset is null)
            throw new CashFlowException(404, "User Asset is not found.");

        var mappedAsset = _mapper.Map<UserAssetForResultDto>(userAsset);

        return mappedAsset;
    }
}
