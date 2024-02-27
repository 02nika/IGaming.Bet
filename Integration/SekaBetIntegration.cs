using Integration.Contracts;

namespace Integration;

public class SekaBetIntegration : ISekaBetIntegration
{
    private readonly Random _random;

    public SekaBetIntegration()
    {
        _random = new Random();
    }
    
    public decimal Bet(decimal amount)
    {
        var coefficient = (decimal)_random.Next(1, 200) / 100;

        return amount * coefficient;
    }
}