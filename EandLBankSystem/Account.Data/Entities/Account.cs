using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Account.Data.Entities;

public class Account
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime OpenDate { get; set; }
    [Required]
    public decimal Balance { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }

}

