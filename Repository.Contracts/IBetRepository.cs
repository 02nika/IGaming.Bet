using Entities.Models;

namespace Repository.Contracts;

public interface IBetRepository
{
    Task<Bet?> GetBetAsync(int betId);
    Task AddBetAsync(Bet bet);
}