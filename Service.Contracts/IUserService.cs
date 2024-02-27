using Shared.Dto.Auth;
using Shared.Dto.User;

namespace Service.Contracts;

public interface IUserService
{
   Task<UserDto> GetUserAsync(int userId);
   Task<UserDto> GetUserAsync(AuthRequest authRequest);
   Task AddUserAsync(AddUserDto userDto);
}