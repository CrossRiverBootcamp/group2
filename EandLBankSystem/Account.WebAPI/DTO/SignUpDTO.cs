using System.ComponentModel.DataAnnotations;

namespace Account.WebAPI.DTO;
public class SignUpDTO
{
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    [MaxLength(50)]
    public string Password { get; set; }
    public string VerificationCode { get; set; }
}

