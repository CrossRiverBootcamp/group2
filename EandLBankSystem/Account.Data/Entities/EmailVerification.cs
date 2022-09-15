using System.ComponentModel.DataAnnotations;

namespace Account.Data.Entities;

public class EmailVerification
{
    [Key, Required, EmailAddress]
    public string Email { get; set; }
    [Required, StringLength(4)]
    public string Code { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime? ExpirationTime { get; set; } = DateTime.UtcNow.AddMinutes(5);
    [Required, Range(0,5)]
    public int numOfTries { get; set; } = 0;
}
