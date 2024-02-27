using Shared.Dto.Balance;

namespace Shared.Dto.User;

public class UserDto
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }

    public BalanceDto Balance { get; set; }
}