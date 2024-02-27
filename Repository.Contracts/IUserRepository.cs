using Entities.Models;
using Shared.Dto.Auth;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task<User?> GetUserAsync(int userId);
    Task<User?> GetUserAsync(AuthRequest auth);
    Task AddUserAsync(User user);
    void UpdateUser(User user);
}