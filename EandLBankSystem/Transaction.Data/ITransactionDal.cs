namespace Transaction.Data
{
    public interface ITransactionDal
    {
        Task<int> PostTransactionAsync(Entities.Transaction transaction);
        Task UpdateTransactionStatusAsync(int transactionId, bool success, string? failureMessage);
    }
}