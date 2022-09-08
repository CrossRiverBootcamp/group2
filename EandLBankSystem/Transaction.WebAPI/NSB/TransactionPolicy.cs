using Account.Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messages.Commands;
using Transaction.Messages.Events;
using Transaction.Service;

namespace Transaction.API.NSB;

public class TransactionPolicy : Saga<TransactionPolicyData>, IAmStartedByMessages<StartTransactionSaga>, IHandleMessages<FinishTransactionSaga>
{
    static ILog log = LogManager.GetLogger<TransactionPolicy>();
    private readonly ITransactionService _transactionService;

    public TransactionPolicy(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task Handle(StartTransactionSaga message, IMessageHandlerContext context)
    {
        log.Info($"Received StartTransactionSaga, Transaction = {message.TransactionId}");

        TransactionSagaStarted transactionSagaStarted = new() {
            TransactionId = message.TransactionId,
            Amount = message.Amount,
            FromAccount = message.FromAccount,
            ToAccount = message.ToAccount
        };

        await context.Publish(transactionSagaStarted);
    }

    public async Task Handle(FinishTransactionSaga message, IMessageHandlerContext context)
    {
        log.Info($"Received FinishTransactionSaga, Transaction = {message.TransactionId}");
        try
        {
            await _transactionService.UpdateTransactionStatus(message.TransactionId, message.Success, message.FailureMessage);
        }
        catch
        {
            MarkAsComplete();
            throw; 
        }
        MarkAsComplete();
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.TransactionId)
                   .ToMessage<StartTransactionSaga>(message => message.TransactionId)
                   .ToMessage<FinishTransactionSaga>(message => message.TransactionId);
    }
    
}