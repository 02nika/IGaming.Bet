namespace Shared.Exceptions.Custom;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base("USER_NOT_FOUND")
    {
    }
}