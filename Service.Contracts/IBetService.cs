using Shared.Dto.Bet;

namespace Service.Contracts;

public interface IBetService
{
    Task<decimal> BetAsync(BetDto betDto, int userId);
    Task<BetDto> GetBetAsync(int betId);
    Task AddBetAsync(AddBetDto betDto);
}