using CashFlow.Service.Dtos.Authentifications;

namespace CashFlow.Service.Interfaces;

public interface IAuthService
{
    public Task<LoginResultDto> AuthenticateAsync(LoginDto login);
}
