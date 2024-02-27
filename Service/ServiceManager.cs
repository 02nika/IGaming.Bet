using AutoMapper;
using Integration.Contracts;
using Microsoft.Extensions.Options;
using Repository.Contracts;
using Service.Contracts;
using Service.Services;
using Shared.Config;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IBetService> _betService;

    public ServiceManager(IRepositoryManager repositoryManager, IIntegrationManager integrationManager,
        IOptions<JwtSettings> jwtSettings, IMapper mapper)
    {
        _authService = new Lazy<IAuthService>(() => new AuthService(jwtSettings));
        _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        _betService = new Lazy<IBetService>(() => new BetService(repositoryManager, integrationManager, mapper));
    }

    public IAuthService AuthService => _authService.Value;
    public IUserService UserService => _userService.Value;
    public IBetService BetService => _betService.Value;
}