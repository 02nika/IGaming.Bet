using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;

namespace Repository.Repositories;

public class BetRepository : RepositoryBase<Bet>, IBetRepository
{
    private readonly AppDbContext _db;

    public BetRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    
    public async Task<Bet?> GetBetAsync(int betId)
    {
        return await FindByCondition(u => u.Id == betId)
            .FirstOrDefaultAsync();
    }

    public async Task AddBetAsync(Bet bet) => await Create(bet);
}