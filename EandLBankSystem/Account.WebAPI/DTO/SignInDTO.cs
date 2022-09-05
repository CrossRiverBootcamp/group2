using System.ComponentModel.DataAnnotations;

namespace Account.WebAPI.DTO;

public class SignInDTO
{
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string Password { get; set; }
}

