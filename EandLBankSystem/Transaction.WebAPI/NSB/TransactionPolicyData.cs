using NServiceBus;

namespace Transaction.API.NSB;

public class TransactionPolicyData : ContainSagaData
{
    public int TransactionId { get; set; }
   
}
