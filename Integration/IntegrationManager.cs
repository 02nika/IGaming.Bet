using Integration.Contracts;

namespace Integration;

public class IntegrationManager : IIntegrationManager
{
    private readonly Lazy<ISekaBetIntegration> _sekaBetService;

    public IntegrationManager()
    {
        _sekaBetService = new Lazy<ISekaBetIntegration>(() => new SekaBetIntegration());
    }

    public ISekaBetIntegration SekaBet => _sekaBetService.Value;
}