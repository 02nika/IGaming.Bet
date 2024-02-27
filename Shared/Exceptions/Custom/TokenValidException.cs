namespace Shared.Exceptions.Custom;

public class TokenValidException : UnauthorizedAccessException
{
    public TokenValidException() : base("TOKEN_NOT_VALID")
    {
    }
}