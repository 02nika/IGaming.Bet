using System.ComponentModel.DataAnnotations;

namespace Shared.Dto.User;

public class AddUserDto
{
    [Required]
    [MinLength(2)]
    public string UserName { get; set; }
    
    [Required]
    [MinLength(5)]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    
    [Required]
    [MinLength(8)]
    [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}