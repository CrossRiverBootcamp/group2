using Transaction.Service.Models;

namespace Transaction.Service
{
    public interface ITransactionService
    {
        Task<bool> PostTransaction(TransactionModel transactionModel);
        Task UpdateTransactionStatus(int transactionId, bool success, string? failureMessage);
    }
}