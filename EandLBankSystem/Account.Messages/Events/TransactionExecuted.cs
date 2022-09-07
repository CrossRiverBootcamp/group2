using NServiceBus;

namespace Account.Messages.Events;

public class TransactionExecuted: IEvent
{
    public int TransactionId { get; set; }
    public bool Success { get; set; }
    public string? FailureMessage { get; set; }
}
