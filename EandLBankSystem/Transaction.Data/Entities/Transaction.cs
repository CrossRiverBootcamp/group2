using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
namespace Transaction.Data.Entities;
public enum EStatus
{
    Proccessing, Success, Fail
}
public class Transaction
{
    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int FromAccount { get; set; }
    [Required]
    public int ToAccount { get; set; }
    [Required, Range(1, 1000000)]
    public int Amount { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime Date { get; set; }
    [Required]
    public EStatus Status { get; set; }
    public string? FailureReason { get; set; }
}
