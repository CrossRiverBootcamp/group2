
namespace Account.Messages.Commands;

public class FinishTransactionSaga
{ 
    public int TransactionId { get; set; }
    public bool Success { get; set; }
    public string? FailureMessage { get; set; }
}
