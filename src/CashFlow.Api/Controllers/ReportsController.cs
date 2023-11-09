using CashFlow.Service.Configurations;
using CashFlow.Service.Dtos.Reports;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IConfiguration _configuration;

        public ReportsController(IReportService reportService, IConfiguration configuration)
        {
            _configuration = configuration;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _reportService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long userId, long id)
            => Ok(await _reportService.RetrieveByIdAsync(userId,id));

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(long userId, long id, [FromBody] ReportForUpdateDto dto)
            => Ok(await _reportService.ModifyAsync(userId,id, dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long userId, long id)
            => Ok(await _reportService.RemoveAsync(userId,id));
    }
}
