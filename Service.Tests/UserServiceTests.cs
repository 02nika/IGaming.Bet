using AutoMapper;
using Entities.Models;
using Moq;
using Repository.Contracts;
using Service.Services;
using Shared.Dto.Auth;
using Shared.Dto.Balance;
using Shared.Dto.User;
using Shared.Exceptions.Custom;

namespace Service.Tests;

public class UserServiceTests
{
    private readonly UserService _sut;
    private readonly Mock<IRepositoryManager> _repositoryManagerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public UserServiceTests()
    {
        _sut = new UserService(_repositoryManagerMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        const int userId = 1;

        var balanceEntity = new Balance
        {
            Id = 1,
            Amount = 1000,
        };

        var userEntity = new User
        {
            Id = userId,
            BalanceId = balanceEntity.Id,
            Username = "nika tsitsilashvili",
            Email = "nikatsitsilashvili1@gmail.com",
            PasswordHash = "SOMERANDOMPASSWORDHASH",
            Balance = balanceEntity,
        };

        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId))
            .ReturnsAsync(userEntity);
        
        _mapperMock.Setup(x => x.Map<UserDto>(It.IsAny<User>()))
            .Returns((User source) =>
            {
                var balanceDto = new BalanceDto
                {
                    Amount = source.Balance.Amount
                };

                return new UserDto
                {
                    Id = source.Id,
                    Username = source.Username,
                    Email = source.Email,
                    Balance = balanceDto
                };
            });
        
        // Act
        var user = await _sut.GetUserAsync(userId);

        // Assert
        Assert.Equal(userId, user.Id);
        Assert.Equal(userEntity.Username, user.Username);
        Assert.Equal(userEntity.Email, user.Email);
        Assert.Equal(balanceEntity.Amount, user.Balance.Amount);
    }
    
    [Fact]
    public async Task GetUserAsync_ShouldThrowException_WhenUserNotExists()
    {
        // Arrange
        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);
        
        // Act
        var exception = await Assert.ThrowsAsync<UserNotFoundException>(async () => { await _sut.GetUserAsync(1); });
        
        // Assert
        Assert.Equal("USER_NOT_FOUND", exception.Message);
    }
    
    [Fact]
    public async Task OtherGetUserAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var authRequest = new AuthRequest
        {
            Username = "nika tsitsilashvili",
            Password = "SolidPassword"
        };
        
        const int userId = 1;

        var balanceEntity = new Balance
        {
            Id = 1,
            Amount = 1000,
        };

        var userEntity = new User
        {
            Id = userId,
            BalanceId = balanceEntity.Id,
            Username = "nika tsitsilashvili",
            Email = "nikatsitsilashvili1@gmail.com",
            PasswordHash = "SOMERANDOMPASSWORDHASH",
            Balance = balanceEntity,
        };

        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(authRequest))
            .ReturnsAsync(userEntity);
        
        _mapperMock.Setup(x => x.Map<UserDto>(It.IsAny<User>()))
            .Returns((User source) =>
            {
                var balanceDto = new BalanceDto
                {
                    Amount = source.Balance.Amount
                };

                return new UserDto
                {
                    Id = source.Id,
                    Username = source.Username,
                    Email = source.Email,
                    Balance = balanceDto
                };
            });
        
        // Act
        var user = await _sut.GetUserAsync(authRequest);

        // Assert
        Assert.Equal(userId, user.Id);
        Assert.Equal(userEntity.Username, user.Username);
        Assert.Equal(userEntity.Email, user.Email);
        Assert.Equal(balanceEntity.Amount, user.Balance.Amount);
    }

    
    [Fact]
    public async Task OtherGetUserAsync_ShouldThrowException_WhenUserNotExists()
    {
        // Arrange
        var authRequest = new AuthRequest
        {
            Username = "not found username",
            Password = "12345678"
        };

        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(authRequest))
            .ReturnsAsync(() => null);
        
        // Act
        var exception = await Assert.ThrowsAsync<UserNotFoundException>(async () => { await _sut.GetUserAsync(authRequest); });
        
        // Assert
        Assert.Equal("USER_NOT_FOUND", exception.Message);
    }
}