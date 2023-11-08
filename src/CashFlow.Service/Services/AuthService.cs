using CashFlow.Service.Dtos.Authentifications;
using CashFlow.Service.Dtos.Users;
using CashFlow.Service.Exceptions;
using CashFlow.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CashFlow.Service.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public AuthService(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }
    public async Task<LoginResultDto> AuthenticateAsync(LoginDto login)
    {
        var user = await _userService.RetrieveByEmailAsync(login.Email);
        if (user is null) // Do logic for cheking password
            throw new CashFlowException(400, "Email or Password in incorrect");

        return new LoginResultDto
        {
            Token = GenerateToken(user)
        };
    }
    private string GenerateToken(UserForResultDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email)

            }),
            Audience = _configuration["JWT:Audience"],
            Issuer = _configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
