using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Interfaces;
using System.Threading.Tasks;
using CashFlow.Service.Configurations;
using System.Reflection.Metadata;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] UserForCreationDto dto)
        => Ok(await _userService.AddAsync(dto));


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _userService.RetrieveAllAsync(@params));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
            => Ok(await _userService.RetrieveByIdAsync(id));


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(await _userService.RemoveAsync(id));

    }
}
