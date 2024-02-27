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
        
        var newAmount = _integrationManager.SekaBet.Bet(betDto.Amount);
        
        user.Balance.Amount += newAmount - betDto.Amount;
        _repositoryManager.UserRepository.UpdateUser(user);

        var bet = _mapper.Map<Bet>(betDto);
        bet.UserId = user.Id;
        bet.WinAmount = newAmount;
        await _repositoryManager.BetRepository.AddBetAsync(bet);
        
        await _repositoryManager.SaveAsync();
        
        return newAmount;
    }
    
    public async Task<BetDto> GetBetAsync(int betId)
    {
        var bet = await _repositoryManager.BetRepository.GetBetAsync(betId);
            
        if (bet is null) throw new BetNotFoundException();

        return _mapper.Map<BetDto>(bet);
    }

    public async Task AddBetAsync(AddBetDto betDto)
    {
        var bet = _mapper.Map<Bet>(betDto);

        await _repositoryManager.BetRepository.AddBetAsync(bet);
        await _repositoryManager.SaveAsync();
    }
}