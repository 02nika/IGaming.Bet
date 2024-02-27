using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("Bets", Schema = "master")]
public class Bet : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal BetAmount { get; set; }
    public decimal WinAmount { get; set; }
    public string Details { get; set; }
    
    public User? User { get; set; }
}