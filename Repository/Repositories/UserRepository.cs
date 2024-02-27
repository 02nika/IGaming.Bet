using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Contracts;
using Shared.Dto.Auth;
using Shared.Extensions;

namespace Repository.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        return await FindByCondition(u => u.Id == userId)
            .Include(u => u.Balance)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserAsync(AuthRequest auth)
    {
        return await _db.Users.Where(u =>
                u.Username == auth.Username &&
                u.PasswordHash == auth.Password.ComputeSha256Hash())
            .Include(u => u.Balance)
            .FirstOrDefaultAsync();
    }

    public void UpdateUser(User user) => Update(user);

    public async Task AddUserAsync(User user) => await Create(user);
}