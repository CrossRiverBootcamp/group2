using NServiceBus;

namespace Account.Messages.Commands;

public class FinishTransactionSaga: ICommand { 
    public int TransactionId { get; set; }
    public bool Success { get; set; }
    public string? FailureMessage { get; set; }
}
