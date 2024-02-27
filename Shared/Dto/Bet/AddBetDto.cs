namespace Shared.Dto.Bet;

public class AddBetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string Details { get; set; }
}