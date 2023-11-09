using Microsoft.AspNetCore.Mvc;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using CashFlow.Service.Configurations;
using Microsoft.AspNetCore.Authorization;
using CashFlow.Service.Dtos.FinancialGoals;

namespace CashFlow.Api.Controllers
{
    public class FinancialGoalsController : BaseController
    {
        private readonly IFinancialGoalService _financialGoalService;
        private readonly IConfiguration _configuration;

        public FinancialGoalsController(IFinancialGoalService financialGoalService, IConfiguration configuration)
        {
            _configuration = configuration;
            _financialGoalService = financialGoalService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] FinancialGoalForCreationDto dto)
        => Ok(await _financialGoalService.AddAsync(dto));


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _financialGoalService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long userId, long id)
            => Ok(await _financialGoalService.RetrieveByIdAsync(userId,id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long userId, long id, [FromBody] FinancialGoalForUpdateDto dto)
            => Ok(await _financialGoalService.ModifyAsync(userId, id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long userId, long id)
            => Ok(await _financialGoalService.RemoveAsync(userId, id));
    }
}
