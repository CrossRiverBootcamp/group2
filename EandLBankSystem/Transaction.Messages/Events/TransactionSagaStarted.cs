using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Messages.Events;

public class TransactionSagaStarted:IEvent
{
    public int TransactionId { get; set; }
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public int Ammount { get; set; }
}
