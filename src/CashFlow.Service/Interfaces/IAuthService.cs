using CashFlow.Service.Dtos.Authentifications;

namespace CashFlow.Service.Interfaces;

public interface IAuthService
{
    public Task<LoginResulDto> AuthenticateAsync(LoginDto login);
}
