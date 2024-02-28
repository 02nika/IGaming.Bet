using AutoMapper;
using Entities.Models;
using Integration.Contracts;
using Moq;
using Repository.Contracts;
using Service.Services;
using Shared.Dto.Bet;
using Shared.Exceptions.Custom;

namespace Service.Tests;

public class BetServiceTests
{
    private readonly BetService _sut;
    private readonly Mock<IRepositoryManager> _repositoryManagerMock = new();
    private readonly Mock<IIntegrationManager> _integrationManagerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    public BetServiceTests()
    {
        _sut = new BetService(_repositoryManagerMock.Object, _integrationManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task BetAsync_ShouldReturnWinAmount_WhenUserExistsAndHasEnoughBalance()
    {
        // Arrange
        var userId = 1;
        var betDto = new BetDto
        {
            Amount = 2,
            Details = "test details"
        };

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

        var winAmount = (decimal)2.5;

        var bet = new Bet();

        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(userId))
            .ReturnsAsync(userEntity);

        _integrationManagerMock.Setup(x => x.SekaBet.Bet(betDto.Amount))
            .Returns(winAmount);

        _repositoryManagerMock.Setup(x => x.UserRepository.UpdateUser(userEntity));

        _mapperMock.Setup(x => x.Map<Bet>(It.IsAny<BetDto>()))
            .Returns((BetDto dto) =>
            {
                bet.BetAmount = dto.Amount;
                bet.Details = dto.Details;

                return bet;
            });

        _repositoryManagerMock.Setup(x => x.BetRepository.AddBetAsync(bet));
        _repositoryManagerMock.Setup(x => x.SaveAsync());

        // Act
        var res = await _sut.BetAsync(betDto, userId);

        // Assert
        Assert.Equal(res, winAmount);
    }

    [Fact]
    public async Task BetAsync_ShouldThrowException_WhenUserNotExists()
    {
        // Arrange
        var userId = 1;
        var betDto = new BetDto
        {
            Amount = 2,
            Details = "test details"
        };

        _repositoryManagerMock.Setup(x => x.UserRepository.GetUserAsync(It.IsAny<int>()))
            .ReturnsAsync(() => null);

        // Act
        var exception = await Assert.ThrowsAsync<UserNotFoundException>(async () =>
        {
            await _sut.BetAsync(betDto, userId);
        });

        // Assert
        Assert.Equal("USER_NOT_FOUND", exception.Message);
    }

    [Fact]
    public async Task BetAsync_ShouldThrowException_WhenAmountNotEnough()
    {
        // Arrange
        var userId = 1;
        var betDto = new BetDto
        {
            Amount = 25,
            Details = "test details"
        };

        var balanceEntity = new Balance
        {
            Id = 1,
            Amount = 10,
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

        // Act
        var exception = await Assert.ThrowsAsync<BalanceNotEnoughException>(async () =>
        {
            await _sut.BetAsync(betDto, userId);
        });

        // Assert
        Assert.Equal("BALANCE_NOT_ENOUGH", exception.Message);
    }
    
    
    [Fact]
    public async Task GetBetAsync_ShouldReturnBetDto_WhenBetExists()
    {
        // Arrange
        var betId = 1;
        var bet = new Bet
        {
            Id = betId,
            UserId = 1,
            BetAmount = 2,
            WinAmount = (decimal)2.5,
            Details = "test"
        };

        _repositoryManagerMock.Setup(x => x.BetRepository.GetBetAsync(betId))
            .ReturnsAsync(bet);

        _mapperMock.Setup(x => x.Map<BetDtoResponse>(It.IsAny<Bet>()))
            .Returns((Bet source) =>
            {
                var betDto = new BetDtoResponse
                {
                    BetAmount = source.BetAmount,
                    WinAmount = source.WinAmount,
                    Details = source.Details
                };

                return betDto;
            });
        
        // Act
        var betDto = await _sut.GetBetAsync(betId);

        // Assert
        Assert.Equal(bet.BetAmount, betDto.BetAmount);
        Assert.Equal(bet.WinAmount, betDto.WinAmount);
        Assert.Equal(bet.Details, betDto.Details);
    }
    
    [Fact]
    public async Task GetBetAsync_ShouldThrowException_WhenBetNotFound()
    {
        // Arrange
        var betId = 1;
        
        _repositoryManagerMock.Setup(x => x.BetRepository.GetBetAsync(betId))
            .ReturnsAsync(() => null);
        
        // Act
        var exception = await Assert.ThrowsAsync<BetNotFoundException>(async () => { await _sut.GetBetAsync(betId); });
        
        // Assert
        Assert.Equal("BET_NOT_FOUND", exception.Message);
    }
}