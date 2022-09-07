using NServiceBus;

namespace Transaction.NSB;

public class TransactionPolicyData : ContainSagaData
{
    public int TransactionId { get; set; }
    public bool SagaTransactionStarted { get; set; }
    public bool SagaTransactionEnded { get; set; }
}
