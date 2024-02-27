namespace Repository.Contracts;

public interface IRepositoryManager
{
    IUserRepository UserRepository { get; }
    IBetRepository BetRepository { get; }
    Task SaveAsync();
}