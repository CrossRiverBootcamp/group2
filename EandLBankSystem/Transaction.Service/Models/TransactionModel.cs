using Transaction.Data.Entities;

namespace Transaction.Service.Models;

public class TransactionModel
{
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public int Amount { get; set; }
    public DateTime? Date { get; set; } = DateTime.UtcNow;
    public EStatus? Status { get; set; } = EStatus.Proccessing;
    public string? FailureReason { get; set; }
}
