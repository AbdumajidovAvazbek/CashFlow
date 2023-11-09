using Microsoft.AspNetCore.Mvc;
using CashFlow.Service.Interfaces;
using CashFlow.WebApi.Controllers;
using CashFlow.Service.Dtos.Authentifications;

namespace CashFlow.Api.Controllers;

public class AuthController: BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("authenticate")]

    public async Task<IActionResult> PostAsync(LoginDto dto)
    {
        var token = await _authService.AuthenticateAsync(dto);

        return Ok(token);
    }
}
