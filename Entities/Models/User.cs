using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("Users", Schema = "master")]
public class User : BaseEntity
{
    public int Id { get; set; }
    
    [ForeignKey(nameof(Balance))]
    public int BalanceId { get; set; }
    
    [MaxLength(100)]
    public string Username { get; set; }
    
    [MaxLength(200)]
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }

    public Balance Balance { get; set; }

    public List<Bet> Bets { get; set; } = new();
}