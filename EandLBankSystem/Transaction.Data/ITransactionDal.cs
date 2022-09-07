namespace Transaction.Data
{
    public interface ITransactionDal
    {
        Task<int> PostTransaction(Entities.Transaction transaction);
        Task UpdateTransactionStatus(int transactionId, bool success, string? failureMessage);
    }
}