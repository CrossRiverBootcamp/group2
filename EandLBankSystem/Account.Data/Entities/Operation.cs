
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Data.Entities;

public class Operation
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int AccountId { get; set; }
    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }
    [Required]
    public int TransactionId { get; set; }
    [Required]
    public bool Credit { get; set; }
    [Required, Range(1, 100000000)]
    public int TransactionAmount { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime OperationTime { get; set; }
}

