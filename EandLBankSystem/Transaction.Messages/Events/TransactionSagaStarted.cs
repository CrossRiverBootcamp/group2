
namespace Transaction.Messages.Events;

public class TransactionSagaStarted
{
    public int TransactionId { get; set; }
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public int Ammount { get; set; }
}
