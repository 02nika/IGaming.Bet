using Repository.Context;
using Repository.Contracts;
using Repository.Repositories;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _appDbContext;
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IBetRepository> _betRepository;

    public RepositoryManager(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_appDbContext));
        _betRepository = new Lazy<IBetRepository>(() => new BetRepository(_appDbContext));
    }

    public IUserRepository UserRepository => _userRepository.Value;
    public IBetRepository BetRepository => _betRepository.Value;

    public async Task SaveAsync() => await _appDbContext.SaveChangesAsync();
}