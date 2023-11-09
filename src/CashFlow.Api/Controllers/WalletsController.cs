using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Dtos.Wallet;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    public class WalletsController : BaseController
    {
        private readonly IWalletService _walletService;
        private readonly IConfiguration _configuration;

        public WalletsController(IWalletService walletService, IConfiguration configuration)
        {
            _configuration = configuration;
            _walletService = walletService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] WalletForCreationDto dto)
        => Ok(await _walletService.AddAsync(dto));


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _walletService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long userId, long id)
            => Ok(await _walletService.RetrieveByIdAsync(userId, id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long userId,long id, [FromBody] WalletForUpdateDto dto)
            => Ok(await _walletService.ModifyAsync(userId,id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long userId, long id)
            => Ok(await _walletService.RemoveAsync(userId,id));

    }
}
