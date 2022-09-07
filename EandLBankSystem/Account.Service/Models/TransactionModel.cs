namespace Account.Service.Models;

public class TransactionModel
{
    public int TransactionId { get; set; }
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public int Amount { get; set; }
}
