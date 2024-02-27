namespace Integration.Contracts;

public interface IIntegrationManager
{
    ISekaBetIntegration SekaBet { get; }
}