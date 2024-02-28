using AutoMapper;
using Entities.Models;
using Integration.Contracts;
using Repository.Contracts;
using Service.Contracts;
using Shared.Dto.Bet;
using Shared.Exceptions.Custom;

namespace Service.Services;

public class BetService : IBetService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IIntegrationManager _integrationManager;
    private readonly IMapper _mapper;

    public BetService(IRepositoryManager repositoryManager, IIntegrationManager integrationManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _integrationManager = integrationManager;
        _mapper = mapper;
    }

    public async Task<decimal> BetAsync(BetDto betDto, int userId)
    {
        var user = await _repositoryManager.UserRepository.GetUserAsync(userId);

        if (user is null) throw new UserNotFoundException();
        
        if (user.Balance.Amount < betDto.Amount) throw new BalanceNotEnoughException();
        
        var winAmount = _integrationManager.SekaBet.Bet(betDto.Amount);

        SetAmount(user, winAmount, betDto.Amount);
        _repositoryManager.UserRepository.UpdateUser(user);

        var bet = BetInstance(betDto, user.Id, winAmount);
        
        await _repositoryManager.BetRepository.AddBetAsync(bet);
        await _repositoryManager.SaveAsync();
        
        return winAmount;
    }

    private void SetAmount(User user, decimal winAmount, decimal betAmount)
    {
        user.Balance.Amount += winAmount - betAmount;
    }

    private Bet BetInstance(BetDto betDto, int userId, decimal winAmount)
    {
        var bet = _mapper.Map<Bet>(betDto);
        bet.UserId = userId;
        bet.WinAmount = winAmount;

        return bet;
    }

    public async Task<BetDtoResponse> GetBetAsync(int betId)
    {
        var bet = await _repositoryManager.BetRepository.GetBetAsync(betId);
            
        if (bet is null) throw new BetNotFoundException();

        return _mapper.Map<BetDtoResponse>(bet);
    }
}