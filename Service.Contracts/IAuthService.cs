using Shared.Dto.Auth;

namespace Service.Contracts;

public interface IAuthService
{
    AuthResponse Auth(int userId);
    int GetUserId(string tokenHash);
}