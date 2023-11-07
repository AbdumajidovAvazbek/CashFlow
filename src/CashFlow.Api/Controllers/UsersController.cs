using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Interfaces;
using System.Threading.Tasks;

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
        {
            var result = await _userService.AddAsync(dto);
            return Ok(result);
        }
    }
}
