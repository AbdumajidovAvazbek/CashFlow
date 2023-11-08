using AutoMapper;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Entities;
using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.UserAssets;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Extensions;
using CashFlow.Service.Helpers;
using CashFlow.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlow.Service.Services
{
    public class UserAssetService : IUserAssetService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<UserAsset> _userAssetRepository;
        private readonly IRepository<User> _userRepository;
        private readonly WebHostEnvironmentHelper _webHostEnvironment;

        public UserAssetService(
            IMapper mapper,
            IRepository<User> userRepository,
            IRepository<UserAsset> userAssetRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userAssetRepository = userAssetRepository;
        }

        public async Task<UserAssetForResultDto> AddAsync(IFormFile file)
        {
            // Logic to add an asset to the system
            var rootPath = Path.Combine(WebHostEnvironmentHelper.WebRootPath, "assets");
            Directory.CreateDirectory(rootPath);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var path = Path.Combine(rootPath, fileName);

            using (var stream = File.OpenWrite(path))
            {
                await file.CopyToAsync(stream);
            }

            var asset = new UserAsset()
            {
                CreatedAt = DateTime.UtcNow,
                Path = Path.Combine("assets", fileName),
                // Set other properties of the asset as needed
            };

            var result = await _userAssetRepository.InsertAsync(asset);

            return _mapper.Map<UserAssetForResultDto>(result);
        }

        public async Task<bool> RemoveAsync(long userId, long id)
        {
            // Logic to remove an asset by ID
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new CashFlowException(404, "User is not found.");
            }

            var userAsset = await _userAssetRepository.SelectAll()
                .Where(ur => ur.Id == id)
                .FirstOrDefaultAsync();

            if (userAsset is null)
            {
                throw new CashFlowException(404, "User Asset is not found.");
            }

            await _userAssetRepository.DeleteAsync(userAsset.Id);

            return true;
        }

        public async Task<IEnumerable<UserAssetForResultDto>> RetrieveAllAsync(long userId, PaginationParams @params)
        {
            // Logic to retrieve all assets for a user with pagination
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new CashFlowException(404, "User is not found.");
            }

            var assets = await _userAssetRepository.SelectAll()
                .ToPagedList(@params)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserAssetForResultDto>>(assets);

        }

        public async Task<UserAssetForResultDto> RetrieveByIdAsync(long userId, long id)
        {
            // Logic to retrieve an asset by ID
            var user = await _userRepository.SelectAll()
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new CashFlowException(404, "User is not found.");
            }

            var userAsset = await _userAssetRepository.SelectAll()
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (userAsset is null)
            {
                throw new CashFlowException(404, "User Asset is not found.");
            }

            var mappedAsset = _mapper.Map<UserAssetForResultDto>(userAsset);

            return mappedAsset;
        }
    }
}
