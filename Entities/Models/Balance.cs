using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;


[Table("Balances", Schema = "master")]
public class Balance : BaseEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }

    public User? User { get; set; }
}