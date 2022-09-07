using Account.Messages.Commands;
using Account.Service;
using Account.Service.Models;
using AutoMapper;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Messages.Events;

namespace Account.NSB;

public class TransactionSagaStartedHandler : IHandleMessages<TransactionSagaStarted>
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    static ILog log = LogManager.GetLogger<TransactionSagaStartedHandler>();

    public TransactionSagaStartedHandler(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }
    public async Task Handle(TransactionSagaStarted message, IMessageHandlerContext context)
    {
        string failureMessage = null;
        bool success = false;
        log.Info($"Transaction {message.TransactionId} has recieved at endpoint.");

        try
        {
            await _accountService.ExecuteTransaction(_mapper.Map<TransactionModel>(message));
            success = true;
            log.Info($"Transaction {message.TransactionId} has completed successfuly.");
        }
        catch (Exception e)
        {
            failureMessage = e.Message;
            success = false;
            log.Error($"Transaction {message.TransactionId} has failed due to Exception: " + failureMessage);
        }

        FinishTransactionSaga finishTransactionSaga = new()
        {
            TransactionId = message.TransactionId,
            Success = success,
            FailureMessage = failureMessage
        };

        await context.Send(finishTransactionSaga);
        await Task.CompletedTask;
    }
}
