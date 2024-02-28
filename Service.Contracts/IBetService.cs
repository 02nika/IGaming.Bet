using Shared.Dto.Bet;

namespace Service.Contracts;

public interface IBetService
{
    Task<decimal> BetAsync(BetDto betDto, int userId);
    Task<BetDtoResponse> GetBetAsync(int betId);
}