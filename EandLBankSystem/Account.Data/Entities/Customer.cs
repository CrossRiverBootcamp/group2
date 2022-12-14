using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Data.Entities;
[Microsoft.EntityFrameworkCore.Index("Email", IsUnique = true)]
public class Customer
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(30)]
    public string FirstName { get; set; }
    [Required, MaxLength(40)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress, MaxLength(50)]
    public string Email { get; set; }
    [Required, MinLength(6), MaxLength(50)]
    public string Password { get; set; }
    [Required]
    [StringLength(8)]
    public string Salt { get; set; }
}

