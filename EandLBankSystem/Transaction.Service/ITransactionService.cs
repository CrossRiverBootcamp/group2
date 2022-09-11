using Transaction.Service.Models;

namespace Transaction.Service
{
    public interface ITransactionService
    {
        Task<bool> PostTransactionAsync(TransactionModel transactionModel);
        Task UpdateTransactionStatusAsync(int transactionId, bool success, string? failureMessage);
    }
}