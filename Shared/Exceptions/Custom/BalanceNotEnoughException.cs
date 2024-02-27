namespace Shared.Exceptions.Custom;

public class BalanceNotEnoughException : BadRequestException
{
    public BalanceNotEnoughException() : base("BALANCE_NOT_ENOUGH")
    {
    }
}