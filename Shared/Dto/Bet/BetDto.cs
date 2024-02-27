using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.Bet;

public class BetDto
{
    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Details { get; set; }
}