using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.FinancialGoals;
using CashFlow.Service.Dtos.Transactions;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;

        public TransactionsController(ITransactionService transactionService, IConfiguration configuration)
        {
            _configuration = configuration;
            _transactionService = transactionService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] TransactionForCreationDto dto)
        => Ok(await _transactionService.AddAsync(dto));


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _transactionService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
            => Ok(await _transactionService.RetrieveByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] TransactionForUpdateDto dto)
            => Ok(await _transactionService.ModifyAsync(id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
            => Ok(await _transactionService.RemoveAsync(id));
    }
}
