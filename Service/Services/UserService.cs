using AutoMapper;
using Entities.Models;
using Repository.Contracts;
using Service.Contracts;
using Shared.Constants;
using Shared.Dto.Auth;
using Shared.Dto.User;
using Shared.Exceptions.Custom;

namespace Service.Services;

public class UserService : IUserService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public UserService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(AuthRequest authRequest)
    {
        var user = await _repositoryManager.UserRepository.GetUserAsync(authRequest);

        if (user is null) throw new UserNotFoundException();
        
        return _mapper.Map<UserDto>(user);
    }
    
    public async Task<UserDto> GetUserAsync(int userId)
    {
        var user = await _repositoryManager.UserRepository.GetUserAsync(userId);
            
        if (user is null) throw new UserNotFoundException();

        return _mapper.Map<UserDto>(user);
    }

    public async Task AddUserAsync(AddUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var balance = new Balance
        {
            Amount = RepositoryConstants.BalanceDefaultAmount
        };

        user.Balance = balance;

        await _repositoryManager.UserRepository.AddUserAsync(user);
        await _repositoryManager.SaveAsync();
    }
}