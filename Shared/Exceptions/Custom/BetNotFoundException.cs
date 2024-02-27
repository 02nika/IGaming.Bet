namespace Shared.Exceptions.Custom;

public class BetNotFoundException : NotFoundException
{
    public BetNotFoundException() : base("BET_NOT_FOUND")
    {
    }
}