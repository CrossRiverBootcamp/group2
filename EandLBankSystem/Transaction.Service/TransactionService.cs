using AutoMapper;
using NServiceBus;
using Transaction.Data;
using Transaction.Messages.Commands;
using Transaction.Service.Models;

namespace Transaction.Service;

public class TransactionService : ITransactionService
{
    private readonly ITransactionDal _transactionDal;
    private readonly IMapper _mapper;
    private readonly IMessageSession _messageSession;

    public TransactionService(ITransactionDal transactionDal, IMessageSession messageSession)
    {
        _transactionDal = transactionDal;
        _mapper = ConfigureAutoMapper();
        _messageSession = messageSession;
    }

    public async Task<bool> PostTransactionAsync(TransactionModel transactionModel)
    {
        try {    
            var transactionId = await _transactionDal.PostTransactionAsync(_mapper.Map<Data.Entities.Transaction>(transactionModel));
            StartTransactionSaga startTransactionSaga = new()
            {
                TransactionId = transactionId,
                Amount = transactionModel.Amount,
                FromAccount = transactionModel.FromAccount,
                ToAccount = transactionModel.ToAccount,
            };
            await _messageSession.Send(startTransactionSaga);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task UpdateTransactionStatusAsync(int transactionId, bool success, string? failureMessage)
    {
        await _transactionDal.UpdateTransactionStatusAsync(transactionId, success, failureMessage);
    }


    private static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TransactionAutoMapper>();
        });
        return config.CreateMapper();
    }
}